import { Options, Vue } from "vue-class-component";
import { ModelSync, Prop, Watch } from "vue-property-decorator";

@Options({
    emits: [
        "update:modelValue",
        "interactionstart",
        "interactionmove",
        "interactionend"
    ]
})
export default class Slider extends Vue {
    @Prop() images!: Array<string>;
    @ModelSync("modelValue", "update:modelValue", { default: 0 }) currentIndex!: number;

    containerElement!: HTMLElement;
    containerMaxChildrenLenght: number = 0;
    previousIndex: number = 0;
    nextIndex: number = 1;
    oldIndex: number = 0;
    onInteractionStartPosition: number = 0;
    changeIndexPercentage: number = 40;
    changeOpacityPercentage: number = 10;
    moving: boolean = false;
    elementOpacity: number = 0;

    @Watch(nameof((slider: Slider) => slider.currentIndex))
    onCurrentIndexChange(index: number, oldIndex: number): void {
        if(!this.moving) {
            const lastElementChild: HTMLElement = this.containerElement.lastElement();
            const nextedLastImage: boolean = index === 0 && oldIndex === this.images.length - 1;
            const previousedFirstImage: boolean = index === this.images.length - 1 && oldIndex === 0;

            if(index > oldIndex && !previousedFirstImage || nextedLastImage) {
                this.changeElementOpacity(lastElementChild, 1);
            } else if(index < oldIndex && !nextedLastImage || previousedFirstImage) {
                this.changeElementOpacity(lastElementChild.previousSiblingElement(), 1);
            }

            this.checkIndexs();

            this.insertImageElements();
        }
    }

    mounted(): void {
        this.containerElement = this.$el as HTMLElement;

        this.containerMaxChildrenLenght = this.containerElement.children.length;

        this.previousIndex = this.images.length - 1;

        this.insertInitialImages();

        window.addEventListener("resize", () => {
            this.containerElement = this.$el as HTMLElement;
            this.containerMaxChildrenLenght = this.containerElement.children.length;
        });
    }

    onInteractionStart(event: Event): void {
        if(event.cancelable) {
            event.preventDefault();
        }

        this.moving = true;

        this.stopTransition();

        if(event instanceof TouchEvent) {
            this.onInteractionStartPosition = event.touches[0].clientX;

            document.addEventListener("touchmove", this.onInteractionMove);
            document.addEventListener("touchend", this.onInteractionEnd);
        } else if(event instanceof MouseEvent) {
            this.onInteractionStartPosition = event.clientX;

            document.onmousemove = this.onInteractionMove;
            document.onmouseup = this.onInteractionEnd;
        }

        this.$emit("interactionstart");
    }

    onInteractionMove(event: Event): void {
        (this.$el as HTMLElement).classList.replace("cursor-grab", "cursor-grabbing");

        let interactionPosition: number = 0;

        if(event instanceof TouchEvent) {
            interactionPosition = event.touches[0].clientX;
        } else if(event instanceof MouseEvent) {
            interactionPosition = event.clientX;
        }

        const position: number = (this.onInteractionStartPosition - interactionPosition) / (this.containerElement.getWidth() / 2);
        const opacity: number = position + this.elementOpacity;
        const changeOpacityValue: number = this.changeOpacityPercentage / 100
        let previousElement: HTMLElement = this.containerElement.lastElement().previousSiblingElement();
        let nextElement: HTMLElement = this.containerElement.lastElement();

        if(this.containerElement.children.length > 3) {
            previousElement = this.containerElement.firstElement().nextSiblingElement();
            nextElement = this.containerElement.lastElement().previousSiblingElement();
        }

        this.removeTransition(nextElement);
        this.removeTransition(previousElement);

        if(opacity > changeOpacityValue) {
            nextElement.style.opacity = `${this.elementOpacity > 0 ? opacity : opacity - changeOpacityValue}`;
            if(this.elementOpacity > 0 ? opacity * 100 : (opacity - changeOpacityValue) * 100 >= this.changeIndexPercentage) {
                this.currentIndex = this.nextIndex;
            } else {
                if(this.currentIndex === this.nextIndex) {
                    this.nextIndex === 0 ? this.currentIndex = this.images.length - 1 : this.currentIndex = this.nextIndex - 1;
                }
            }
        } else if(opacity < -changeOpacityValue) {
            previousElement.style.opacity = `${Math.abs(this.elementOpacity < 0 ? opacity : opacity + changeOpacityValue)}`;
            if(Math.abs(this.elementOpacity < 0 ? opacity * 100 : (opacity + changeOpacityValue) * 100) >= this.changeIndexPercentage) {
                this.currentIndex = this.previousIndex;
            } else {
                if(this.currentIndex === this.previousIndex) {
                    this.previousIndex === this.images.length - 1 ? this.currentIndex = 0 : this.currentIndex = this.previousIndex + 1;
                }
            }
        }

        this.$emit("interactionmove", position * 100, this.previousIndex, this.nextIndex);
    }

    onInteractionEnd(): void {
        (this.$el as HTMLElement).classList.replace("cursor-grabbing", "cursor-grab");
        this.moving = false;

        if(this.containerElement.children.length > 3) {
            this.containerElement.removeChild(this.containerElement.lastElement());
        }

        const lastElementChild: HTMLElement = this.containerElement.lastElement();

        this.addTransition(lastElementChild);
        this.addTransition(lastElementChild.previousSiblingElement());

        if(this.currentIndex === this.nextIndex) {
            this.changeElementOpacity(lastElementChild, 1);
        } else if(this.currentIndex === this.previousIndex) {
            this.changeElementOpacity(lastElementChild.previousSiblingElement(), 1);
        } else {
            this.changeElementOpacity(lastElementChild, 0);
            this.changeElementOpacity(lastElementChild.previousSiblingElement(), 0);
        }

        this.$emit("interactionend");

        document.removeEventListener("touchmove", this.onInteractionMove);
        document.removeEventListener("touchend", this.onInteractionEnd);
        document.onmousemove = null;
        document.onmouseup = null;
    }

    changeImageThroughPercentage(percentage: number): void {
        this.moving = true;

        const itemPercentage: number = 100 / (this.images.length - 1);
        const percentageConverted: number = percentage / itemPercentage;
        let index: number = Math.trunc(percentageConverted);

        if(percentageConverted >= this.images.length - 1) {
            index = this.images.length - 2
        } else if(percentageConverted <= 0) {
            index = 0;
        }

        const opacity: number = percentageConverted - index;

        if(this.containerElement.children.length > 3) {
            this.containerElement.removeChild(this.containerElement.lastElement());
        }

        this.removeTransition(this.containerElement.lastElement().previousSiblingElement());
        this.removeTransition(this.containerElement.lastElement());

        if(this.images[index] !== (this.containerElement.firstElementChild as HTMLImageElement).src) {
            this.containerElement.removeChild(this.containerElement.firstElement());
            this.containerElement.removeChild(this.containerElement.lastElement().previousSiblingElement());
            this.containerElement.removeChild(this.containerElement.lastElement());

            this.createImageElement(index, "none", 1);
            this.createImageElement(index === 0 ? this.images.length - 1 : index - 1, "none", 0);
            this.createImageElement(index + 1, "none", 0);
        }

        this.containerElement.lastElement().style.opacity = `${opacity}`;
    }

    adjustElementsThroughIndex(): void {
        this.moving = false;

        this.addTransition(this.containerElement.lastElement());
        this.addTransition(this.containerElement.lastElement().previousSiblingElement());

        if(this.images[this.currentIndex] !== (this.containerElement.firstChild as HTMLImageElement).src) {
            this.changeElementOpacity(this.containerElement.lastElement(), 1);
        } else {
            this.changeElementOpacity(this.containerElement.lastElement(), 0);
            this.changeElementOpacity(this.containerElement.lastElement().previousSiblingElement(), 0);
        }
    }

    onTransitionStart(event: Event): void {
        const didntChange: boolean = this.currentIndex === this.oldIndex;
        const next: boolean = (this.oldIndex < this.currentIndex || (this.oldIndex === this.images.length - 1 && this.currentIndex === 0)) && !(this.oldIndex === 0 && this.currentIndex === this.images.length -1);

        if(!didntChange) {
            (event.target as HTMLElement).addEventListener("transitionend", () => {
                if((event.target as HTMLElement).style.opacity !== "0") {
                    this.containerElement.removeChild(this.containerElement.firstElement());

                    next
                        ? this.containerElement.removeChild(this.containerElement.firstElement())
                        : this.containerElement.removeChild(this.containerElement.firstElement().nextSiblingElement());

                    if(this.containerElement.children.length < 3) {
                        this.checkIndexs();
                        this.insertImageElements();
                    }
                }
            });
        }

        this.oldIndex = this.currentIndex;
    }

    insertInitialImages(): void {
        (this.containerElement.children[0] as HTMLImageElement).src = this.images[this.currentIndex];
        (this.containerElement.children[1] as HTMLImageElement).src = this.images[this.previousIndex];
        (this.containerElement.children[1] as HTMLElement).style.opacity = "0";
        (this.containerElement.children[2] as HTMLImageElement).src = this.images[this.nextIndex];
        (this.containerElement.children[2] as HTMLElement).style.opacity = "0";
    }

    insertImageElements(): void {
        this.createImageElement(this.previousIndex, "opacity 0.5s", 0);
        this.createImageElement(this.nextIndex, "opacity 0.5s", 0);
    }

    createImageElement(index: number, transition: string, opacity: number): void {
        const img: HTMLImageElement = document.createElement("img");
        img.src = this.images[index];
        this.containerElement.appendChild(img);
        img.style.transition = transition;
        img.style.opacity = `${opacity}`;
    }

    changeElementOpacity(element: HTMLElement, opacity: number): void {
        if(element.style.opacity < "1") {
            element.style.opacity = `${opacity}`;
        } else {
            this.containerElement.removeChild(this.containerElement.firstElement());

            this.images[this.currentIndex] === (this.containerElement.firstElementChild as HTMLImageElement).src
                ? this.containerElement.removeChild(this.containerElement.firstElement().nextSiblingElement())
                : this.containerElement.removeChild(this.containerElement.firstElement());

            if(this.containerElement.children.length < 3) {
                this.checkIndexs();
                this.insertImageElements();
            }

            this.oldIndex = this.currentIndex;
        }
    }

    addTransition(element: HTMLElement): void {
        element.style.transition = "opacity 0.5s";
    }

    removeTransition(element: HTMLElement): void {
        element.style.transition = "none";
    }

    stopTransition(): void {
        this.moving = true;

        let previousElement: HTMLElement = this.containerElement.lastElement().previousSiblingElement();
        let nextElement: HTMLElement = this.containerElement.lastElement();

        if(this.containerElement.children.length > 3) {
            this.containerElement.removeChild(this.containerElement.lastElement().previousSiblingElement());
            previousElement = this.containerElement.firstElement().nextSiblingElement();
            nextElement = this.containerElement.lastElement().previousSiblingElement();

            if(this.images[this.currentIndex] === (previousElement as HTMLImageElement).src) {
                this.currentIndex = this.nextIndex;
            } else if(this.images[this.currentIndex] === (nextElement as HTMLImageElement).src) {
                this.currentIndex = this.previousIndex;
            }

            this.$nextTick(() => {
                this.checkIndexs();
            });
        }

        const previousElementOpacity: number = Number(window.getComputedStyle(previousElement).opacity);
        previousElement.style.opacity = `${previousElementOpacity}`;
        this.removeTransition(previousElement);

        const nextElementOpacity: number = Number(window.getComputedStyle(nextElement).opacity);
        nextElement.style.opacity = `${nextElementOpacity}`;
        this.removeTransition(nextElement);

        this.elementOpacity = previousElementOpacity > nextElementOpacity ? -previousElementOpacity : nextElementOpacity;
    }

    checkIndexs(): void {
        this.currentIndex === 0 ? this.previousIndex = this.images.length - 1 : this.previousIndex = this.currentIndex - 1;
        this.currentIndex === this.images.length - 1 ? this.nextIndex = 0 : this.nextIndex = this.currentIndex + 1;
    }
}
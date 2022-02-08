import { Options, Vue } from "vue-class-component";
import { ModelSync, Prop, Ref, Watch } from "vue-property-decorator";

@Options({
    emits: [
        "update:modelValue",
        "interactionstart",
        "interactionmove",
        "interactionend"
    ]
})
export default class Carousel extends Vue {
    @ModelSync("modelValue", "update:modelValue") currentIndex!: number;
    @Ref() containerElement!: HTMLElement;
    @Prop({default: false}) centerSelected!: boolean;

    inicialPosition: number = 0;
    containerElementWidth: number = 0;
    childElementWidth: number = 0;
    spaceBetweenChilds: number = 0;
    onInteractionStartPosition: number = 0;
    elementPosition: number = 0;
    rightExtreme: number = 0;
    changeIndexPercentage: number = 40;
    animationClasses: Array<string> = [];
    moving: boolean = false;
    getElementPositionWhenStopped: boolean = false;

    @Watch(nameof((carousel: Carousel) => carousel.currentIndex))
    onCurrentIndexChange(): void {
        if(!this.moving) {
            this.translateThroughIndex();
        }
    }

    mounted(): void {
        this.setup();

        window.addEventListener("resize", this.setup);
    }

    setup(): void {
        const lastChild: HTMLElement = this.containerElement.lastElement();
        this.childElementWidth = lastChild.getWidth();

        if(this.centerSelected && this.containerElement.parentElement) {
            this.inicialPosition = (this.containerElement.parentElement.getWidth() / 2) - (this.childElementWidth / 2);
            this.translateX(this.inicialPosition);
        }

        this.spaceBetweenChilds = lastChild.getMarginRight() + lastChild.getMarginLeft();

        this.containerElementWidth = this.getContainerElementWidth();

        const elementLeftPosition: number = this.containerElement.getLeftPosition() - this.containerElement.getMarginLeft();
        const elementRightPosition: number = window.innerWidth - elementLeftPosition - this.containerElement.getWidth();

        const windowBiggerThanContainer: boolean = Math.abs(this.containerElementWidth) + this.inicialPosition < window.innerWidth;
        this.rightExtreme = windowBiggerThanContainer
            ? 0
            : ((window.innerWidth + this.containerElementWidth) - (elementRightPosition + elementLeftPosition)) - this.inicialPosition;

        this.animationClasses = this.getAnimationClasses();
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
        event.preventDefault();
        (this.$el as HTMLElement).classList.replace("cursor-grab", "cursor-grabbing");

        let interactionPosition: number = 0;

        if(event instanceof TouchEvent) {
            interactionPosition = event.touches[0].clientX;
        } else if(event instanceof MouseEvent) {
            interactionPosition = event.clientX;
        }

        const position: number = this.elementPosition + (interactionPosition - this.onInteractionStartPosition);
        let translateX: number = position;

        if(position > this.inicialPosition) {
            translateX = (position / 50) + this.inicialPosition;
        } else if(position < this.rightExtreme) {
            translateX = this.rightExtreme + ((position - this.rightExtreme) / 50);
        }

        this.translateX(translateX);

        this.currentIndex = this.getCurrentIndex(translateX);

        const percentageMoved: number = ((position - this.inicialPosition) / (this.containerElementWidth + this.childElementWidth)) * 100;

        this.$emit("interactionmove", percentageMoved);
    }

    onInteractionEnd(): void {
        this.moving = false;
        (this.$el as HTMLElement).classList.replace("cursor-grabbing", "cursor-grab");

        this.containerElement.classList.add(...this.animationClasses);

        this.translateThroughIndex();

        this.$emit("interactionend");

        document.removeEventListener("touchmove", this.onInteractionMove);
        document.removeEventListener("touchend", this.onInteractionEnd);
        document.onmousemove = null;
        document.onmouseup = null;
    }

    translateThroughElement(element: HTMLElement): void {
        this.containerElement.classList.add(...this.animationClasses);

        this.currentIndex = Array.from(this.containerElement.children).indexOf(element);
    }

    translateThroughIndex(): void {
        const position: number = (this.currentIndex * -(this.childElementWidth + this.spaceBetweenChilds)) + this.inicialPosition;
        const translateX: number = position < this.rightExtreme || this.getElementPosition() < this.rightExtreme
            ? this.rightExtreme
            : position;

        this.translateX(translateX);
    }

    translateThroughPositionPercentage(positionPercentage: number, minIndex: number, maxIndex: number): void {
        this.moving = true;

        if(!this.getElementPositionWhenStopped) {
            this.stopTransition();
            this.getElementPositionWhenStopped = true;
        }

        const position: number = ((positionPercentage / 100) * this.containerElementWidth) + this.elementPosition;
        let translateX: number = position;

        this.containerElement.classList.remove(...this.animationClasses);

        if(translateX <= -((maxIndex * this.childElementWidth) - this.inicialPosition) && maxIndex !== 0) {
            translateX = -((maxIndex * this.childElementWidth) - this.inicialPosition);
        } else if(translateX >= -((minIndex * this.childElementWidth) - this.inicialPosition) && minIndex !== this.containerElement.children.length - 1) {
            translateX = -((minIndex * this.childElementWidth) - this.inicialPosition);
        } else if(position > this.inicialPosition) {
            this.containerElement.classList.add(...this.animationClasses);
            translateX = position > (this.changeIndexPercentage / 100) * this.childElementWidth
                ? this.rightExtreme
                : this.inicialPosition;
        } else if(position < this.rightExtreme) {
            this.containerElement.classList.add(...this.animationClasses);
            translateX = position < this.rightExtreme - ((this.changeIndexPercentage / 100) * this.childElementWidth)
                ? this.inicialPosition
                : this.rightExtreme;
        }

        this.translateX(translateX);
    }

    alignTranslate(): void {
        this.moving = false;
        this.getElementPositionWhenStopped = false;

        this.containerElement.classList.add(...this.animationClasses);

        this.translateThroughIndex();
    }

    getCurrentIndex(translateX: number): number {
        const translate: number = translateX - this.inicialPosition;
        const childSizePercentageToAvoid: number = (100 - this.changeIndexPercentage) / 100;
        const spaceToAvoid: number = this.spaceBetweenChilds + (this.childElementWidth * childSizePercentageToAvoid);
        const convertToPercentage: number = (translate - spaceToAvoid) / this.containerElementWidth;
        const convertToChildSizePercentage: number = convertToPercentage * this.containerElement.children.length;
        return translateX <= this.rightExtreme ? this.containerElement.children.length - 1 : Math.trunc(convertToChildSizePercentage);
    }

    getElementPosition(): number {
        const matrix: DOMMatrixReadOnly = new DOMMatrixReadOnly(getComputedStyle(this.containerElement).transform);
        return matrix.m41;
    }

    getContainerElementWidth(): number {
        const width: number = ([...this.containerElement.children] as Array<HTMLElement>)
            .map((el: HTMLElement) => el.offsetWidth)
            .reduce((total: number, value: number) => total + value, 0);
        return -(width + (this.spaceBetweenChilds * (this.containerElement.children.length - 1)) + (this.containerElement.getMarginRight() + this.containerElement.getMarginLeft()));
    }

    getAnimationClasses(): Array<string> {
        const animationClasses: Array<string> = [];
        for(const className of this.containerElement.classList) {
            const classes: string | undefined = className.match("transition")?.input || className.match("duration")?.input;
            if(classes) {
                animationClasses.push(classes);
            }
        }
        return animationClasses;
    }

    stopTransition(): void {
        this.moving = true;
        this.elementPosition = this.getElementPosition();
        this.translateX(this.elementPosition);
        this.containerElement.classList.remove(...this.animationClasses);
    }

    translateX(value: number): void {
        this.containerElement.style.transform = `translateX(${value}px)`;
    }
}
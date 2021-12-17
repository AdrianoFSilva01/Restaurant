import { Options, Vue } from "vue-class-component";
import { ModelSync, Ref, Watch } from "vue-property-decorator";

@Options({
    emits: [
        "update:modelValue",
        "touchstart",
        "touchmove",
        "touchend"
    ]
})
export default class Carousel extends Vue {
    @ModelSync("modelValue", "update:modelValue") currentIndex!: number;
    @Ref() containerElement!: HTMLElement;

    containerElementWidth: number = 0;
    childElementWidth: number = 0;
    spaceBetweenChilds: number = 0;
    onTouchStartPosition: number = 0;
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
        const lastChild: HTMLElement = this.containerElement.lastElement();
        this.childElementWidth = lastChild.getWidth();

        this.spaceBetweenChilds = lastChild.getMarginRight() + lastChild.getMarginLeft();

        this.containerElementWidth = this.getContainerElementWidth();

        const elementLeftPosition: number = this.containerElement.getLeftPosition() - this.containerElement.getMarginLeft();
        const elementRightPosition: number = window.innerWidth - elementLeftPosition - this.containerElement.getWidth();

        const windowBiggerThanContainer: boolean = Math.abs(this.containerElementWidth) < window.innerWidth;
        this.rightExtreme = windowBiggerThanContainer
            ? 0
            : (window.innerWidth + this.containerElementWidth) - (elementRightPosition + elementLeftPosition);

        this.animationClasses = this.getAnimationClasses();
    }

    onTouchStart(touchEvent: TouchEvent): void {
        if(touchEvent.cancelable) {
            touchEvent.preventDefault();
        }

        this.moving = true;

        this.stopTransition();

        this.onTouchStartPosition = touchEvent.touches[0].clientX;

        document.ontouchmove = this.onTouchMove;
        document.ontouchend = this.onTouchEnd;

        this.$emit("touchstart");
    }

    onTouchMove(touchEvent: TouchEvent): void {
        touchEvent.preventDefault();

        const position: number = this.elementPosition + (touchEvent.touches[0].clientX - this.onTouchStartPosition)
        let translateX: number = position;

        if(position > 0) {
            translateX = position / 50;
        } else if(position < this.rightExtreme) {
            translateX = this.rightExtreme + ((position - this.rightExtreme) / 50);
        }

        this.translateX(translateX);

        this.currentIndex = this.getCurrentIndex(translateX);

        const percentageMoved: number = (position / (this.containerElementWidth + this.childElementWidth)) * 100;

        this.$emit("touchmove", percentageMoved);
    }

    onTouchEnd(): void {
        this.moving = false;

        this.containerElement.classList.add(...this.animationClasses);

        this.translateThroughIndex();

        this.$emit("touchend");

        document.ontouchmove = null;
        document.ontouchend = null;
    }

    translateThroughElement(element: HTMLElement): void {
        this.containerElement.classList.add(...this.animationClasses);

        this.currentIndex = Array.from(this.containerElement.children).indexOf(element);
    }

    translateThroughIndex(): void {
        const position: number = this.currentIndex * -(this.childElementWidth + this.spaceBetweenChilds);
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

        if(translateX <= -(maxIndex * this.childElementWidth) && maxIndex !== 0) {
            translateX = -(maxIndex * this.childElementWidth);
        } else if(translateX >= -(minIndex * this.childElementWidth) && minIndex !== this.containerElement.children.length - 1) {
            translateX = -(minIndex * this.childElementWidth);
        } else if(position > 0) {
            this.containerElement.classList.add(...this.animationClasses);
            translateX = position > (this.changeIndexPercentage / 100) * this.childElementWidth
                ? this.rightExtreme
                : 0;
        } else if(position < this.rightExtreme) {
            this.containerElement.classList.add(...this.animationClasses);
            translateX = position < this.rightExtreme - ((this.changeIndexPercentage / 100) * this.childElementWidth)
                ? 0
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
        const childSizePercentageToAvoid: number = (100 - this.changeIndexPercentage) / 100;
        const spaceToAvoid: number = this.spaceBetweenChilds + (this.childElementWidth * childSizePercentageToAvoid);
        const convertToPercentage: number = (translateX - spaceToAvoid) / this.containerElementWidth;
        const convertToChildSizePercentage: number = convertToPercentage * this.containerElement.children.length;
        return Math.trunc(convertToChildSizePercentage);
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
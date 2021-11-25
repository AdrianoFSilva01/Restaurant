import { Vue } from "vue-class-component";
import { Ref } from "vue-property-decorator";

export default class Carousel extends Vue {
    @Ref() containerElement!: HTMLElement;

    currentIndex: number = 0;
    containerElementWidth: number = 0;
    childElementWidth: number = 0;
    spaceBetweenChilds: number = 0;
    onTouchStartPosition: number = 0;
    elementPosition: number = 0;
    rightExtreme: number = 0;
    changeIndexPercentage: number = 40;
    animationClasses: Array<string> = [];

    mounted(): void {
        this.containerElementWidth = this.getContainerElementWidth();

        const lastChild: CSSStyleDeclaration = getComputedStyle(this.containerElement.lastElementChild as Element);
        this.childElementWidth = parseFloat(lastChild.width);
        this.spaceBetweenChilds = parseInt(lastChild.marginRight) + parseInt(lastChild.marginLeft);

        const windowBiggerThanContainer: boolean = Math.abs(this.containerElementWidth) < window.innerWidth;
        this.rightExtreme = windowBiggerThanContainer ? 0 : window.innerWidth + this.containerElementWidth;

        this.animationClasses = this.getAnimationClasses();
    }

    set translateX(value: number) {
        this.containerElement.style.transform = `translateX(${value}px)`;
    }

    onTouchStart(touchEvent: TouchEvent): void {
        touchEvent.preventDefault();

        this.stopTransition();

        this.onTouchStartPosition = touchEvent.touches[0].clientX;

        document.ontouchmove = this.onTouchMove;
        document.ontouchend = this.onTouchEnd;
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

        this.translateX = translateX;

        this.currentIndex = this.getCurrentIndex(translateX);

        document.ontouchend = this.onTouchEnd;
    }

    onTouchEnd(): void {
        this.containerElement.classList.add(...this.animationClasses);

        const position: number = this.currentIndex * -(this.childElementWidth + this.spaceBetweenChilds);
        const translateX: number = position < this.rightExtreme || this.getElementPosition() < this.rightExtreme ? this.rightExtreme : position;

        this.translateX = translateX;

        document.ontouchmove = null;
        document.ontouchend = null;
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
        const marginLeft: number = parseInt(window.getComputedStyle(this.containerElement).marginLeft);
        const marginRight: number = parseInt(window.getComputedStyle(this.containerElement).marginRight);
        return -this.containerElement.getBoundingClientRect().width - (marginLeft + marginRight);
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
        this.elementPosition = this.getElementPosition();
        this.translateX = this.elementPosition;
        this.containerElement.classList.remove(...this.animationClasses);
    }
}
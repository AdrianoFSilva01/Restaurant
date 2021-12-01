import { Options, Vue } from "vue-class-component";
import { ModelSync, Prop, Ref, Watch } from "vue-property-decorator";

@Options({
    emits: [
        "update:modelValue"
    ]
})
export default class Carousel<T> extends Vue {
    @ModelSync("modelValue", "update:modelValue") currentIndex!: number;
    @Prop() source!: T;
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

    @Watch(nameof((carousel: Carousel<T>) => carousel.currentIndex))
    onCurrentIndexChange(): void {
        if(!this.moving) {
            const position: number = this.currentIndex * -(this.childElementWidth + this.spaceBetweenChilds);
            const translateX: number = position < this.rightExtreme || this.getElementPosition() < this.rightExtreme ? this.rightExtreme : position;

            this.translateX = translateX;
        }
    }

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
        this.moving = true;

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
        this.moving = false;

        this.containerElement.classList.add(...this.animationClasses);

        const position: number = this.currentIndex * -(this.childElementWidth + this.spaceBetweenChilds);
        const translateX: number = position < this.rightExtreme || this.getElementPosition() < this.rightExtreme ? this.rightExtreme : position;

        this.translateX = translateX;

        document.ontouchmove = null;
        document.ontouchend = null;
    }

    translateThroughElement(element: HTMLElement): void {
        this.containerElement.classList.add(...this.animationClasses);

        this.currentIndex = Array.from(this.containerElement.children).indexOf(element);
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
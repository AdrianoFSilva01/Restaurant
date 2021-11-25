import { Vue } from "vue-class-component";
import { Prop, Ref } from "vue-property-decorator";

export default class Slider extends Vue {
    @Ref() previousImageElement!: HTMLImageElement;
    @Ref() currentImageElement!: HTMLImageElement;
    @Ref() nextImageElement!: HTMLImageElement;

    @Prop() images!: Array<string>;

    previousIndex: number = 0;
    currentIndex: number = 0;
    nextIndex: number = 1;

    mounted(): void {
        this.previousIndex = this.images.length - 1;

        this.changeElementsImage();
    }

    nextImage(): void {
        this.currentIndex === this.images.length - 1 ? this.currentIndex = 0 : this.currentIndex++;

        this.replaceClasses(this.nextImageElement, [["transition-none", "transition-opacity"], ["opacity-0", "opacity-100"]]);
        this.checkIndexs();
    }

    previousImage(): void {
        this.currentIndex === 0 ? this.currentIndex = this.images.length - 1 : this.currentIndex--;

        this.replaceClasses(this.currentImageElement, [["transition-none", "transition-opacity"], ["opacity-100", "opacity-0"]]);
        this.checkIndexs();
    }

    onTransitionEnd(): void {
        this.replaceClasses(this.currentImageElement, [["transition-opacity", "transition-none"], ["opacity-0", "opacity-100"]]);
        this.replaceClasses(this.nextImageElement, [["transition-opacity", "transition-none"], ["opacity-100", "opacity-0"]]);

        this.changeElementsImage();
    }

    changeElementsImage(): void {
        this.currentImageElement.src = this.images[this.currentIndex];
        this.previousImageElement.src = this.images[this.previousIndex];
        this.nextImageElement.src = this.images[this.nextIndex];
    }

    replaceClasses(element: HTMLElement, classes: Array<[string, string]>): void {
        for(const className of classes) {
            element.classList.replace(className[0], className[1]);
        }
    }

    checkIndexs(): void {
        this.currentIndex === 0 ? this.previousIndex = this.images.length - 1 : this.previousIndex = this.currentIndex - 1;
        this.currentIndex === this.images.length - 1 ? this.nextIndex = 0 : this.nextIndex = this.currentIndex + 1;
    }
}
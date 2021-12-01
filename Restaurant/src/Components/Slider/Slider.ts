import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";

export default class Slider extends Vue {
    @Prop() images!: Array<string>;

    containerElement!: HTMLElement;
    containerMaxChildrenLenght: number = 0;
    previousIndex: number = 0;
    currentIndex: number = 0;
    nextIndex: number = 1;

    mounted(): void {
        this.containerElement = this.$el as HTMLElement;
        this.containerMaxChildrenLenght = this.containerElement.children.length;

        this.previousIndex = this.images.length - 1;

        this.insertInitialImages();
    }

    changeImage(next: boolean): void {
        const lastElementChild: HTMLElement = this.containerElement.lastElementChild as HTMLElement;

        if(next) {
            this.currentIndex === this.images.length - 1 ? this.currentIndex = 0 : this.currentIndex++;
            this.changeElementOpacity(lastElementChild);
            this.containerElement.removeChild(lastElementChild.previousElementSibling as HTMLElement);
        } else {
            this.currentIndex === 0 ? this.currentIndex = this.images.length - 1 : this.currentIndex--;
            this.changeElementOpacity(lastElementChild.previousElementSibling as HTMLElement);
            this.containerElement.removeChild(lastElementChild);
        }

        this.checkIndexs();

        this.insertImageElements();
    }

    onTransitionEnd(): void {
        this.containerElement.removeChild(this.containerElement.firstElementChild as HTMLElement);
    }

    insertInitialImages(): void {
        (this.containerElement.children[0] as HTMLImageElement).src = this.images[this.currentIndex];
        (this.containerElement.children[1] as HTMLImageElement).src = this.images[this.previousIndex];
        (this.containerElement.children[2] as HTMLImageElement).src = this.images[this.nextIndex];
    }

    insertImageElements(): void {
        const previousImg: HTMLImageElement = document.createElement("img");
        previousImg.src = this.images[this.previousIndex];
        this.containerElement.appendChild(previousImg);
        previousImg.classList.add("opacity-0");

        const nextImg: HTMLImageElement = document.createElement("img");
        nextImg.src = this.images[this.nextIndex];
        this.containerElement.appendChild(nextImg);
        nextImg.classList.add("opacity-0");
    }

    changeElementOpacity(element: HTMLElement): void {
        element.classList.replace("opacity-0", "opacity-100");
    }

    checkIndexs(): void {
        this.currentIndex === 0 ? this.previousIndex = this.images.length - 1 : this.previousIndex = this.currentIndex - 1;
        this.currentIndex === this.images.length - 1 ? this.nextIndex = 0 : this.nextIndex = this.currentIndex + 1;
    }
}
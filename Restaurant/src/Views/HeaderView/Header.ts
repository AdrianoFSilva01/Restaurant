import { Options, Vue } from "vue-class-component";
import { Ref } from "vue-property-decorator";

@Options({
    emits: [
        "hamburger-menu-clicked"
    ]
})
export default class Header extends Vue {
    @Ref() imageElement!: HTMLElement;

    element!: HTMLElement;
    elementPadding: number = 0;
    elementHeight: number = 0;
    scrollPosition: number = 0;
    elementTransition: string = "";
    imagePaddingWhenSticky: number = 12;

    mounted(): void {
        this.element = this.$el as HTMLElement;
        this.elementHeight = this.element.getHeight();
        this.elementPadding = this.element.getPaddingTop();
        this.elementTransition = this.element.getTransition();

        window.addEventListener("scroll", () => {
            this.element.style.transition = this.elementTransition;

            if(this.scrollPosition < window.scrollY) {
                this.element.style.top = "-100%";
            } else if(this.scrollPosition > window.scrollY) {
                if(window.scrollY > this.elementHeight * 2) {
                    this.imageElement.style.padding = `${this.imagePaddingWhenSticky}px`;
                    this.element.style.paddingTop = "0";
                    this.element.style.paddingBottom = "0";
                    this.element.style.top = "0px";
                    this.element.classList.add("shadow-md");
                } else if(window.scrollY <= this.elementHeight && window.scrollY >= 0) {
                    const percentage: number = (window.scrollY / this.elementHeight);
                    const imageElementPadding: number = percentage * this.imagePaddingWhenSticky;
                    const padding: number = (1 - percentage) * this.elementPadding;

                    this.element.style.transition = "none";

                    this.element.style.paddingTop = `${padding}px`;
                    this.element.style.paddingBottom = `${padding}px`;
                    this.imageElement.style.padding = `${imageElementPadding}px`;
                    this.element.classList.remove("shadow-md");
                }
            }

            this.scrollPosition = window.scrollY;
        });
    }

    hamburgerMenuOnClick(): void {
        this.$emit("hamburger-menu-clicked");
    }
}
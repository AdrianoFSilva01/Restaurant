import WaveButton from "@/Components/WaveButton/WaveButton.vue";
import { Options, Vue } from "vue-class-component";
import { Ref } from "vue-property-decorator";

@Options({
    components: {
        WaveButton
    },
    emits: [
        "hamburger-menu-clicked"
    ]
})
export default class Header extends Vue {
    @Ref() headerContainer!: HTMLElement;
    @Ref() imageElement!: HTMLElement;

    elementPadding: number = 0;
    elementHeight: number = 0;
    scrollPosition: number = 0;
    elementTransition: string = "";
    imagePaddingWhenSticky: number = 12;

    mounted(): void {
        this.elementHeight = this.headerContainer.getHeight();
        this.elementPadding = this.headerContainer.getPaddingTop();
        this.elementTransition = this.headerContainer.getTransition();

        (this.$el as HTMLElement).style.height = `${this.elementHeight}px`;

        window.addEventListener("scroll", () => {
            this.headerContainer.style.transition = this.elementTransition;

            if(this.scrollPosition < window.scrollY) {
                if(window.scrollY < this.elementHeight) {
                    this.headerContainer.style.transition = "none";
                    this.headerContainer.style.transform = `translateY(-${window.scrollY}px)`;
                } else {
                    this.headerContainer.style.transform = "translateY(-100%)";
                }
            } else if(this.scrollPosition > window.scrollY) {
                if(window.scrollY > this.elementHeight * 2) {
                    this.imageElement.style.padding = `${this.imagePaddingWhenSticky}px`;
                    this.headerContainer.style.paddingTop = "0";
                    this.headerContainer.style.paddingBottom = "0";
                    this.headerContainer.style.transform = "translateY(0)";
                    this.headerContainer.classList.add("shadow-md");
                } else if(window.scrollY <= this.elementHeight && window.scrollY >= 0) {
                    const percentage: number = (window.scrollY / this.elementHeight);
                    const imageElementPadding: number = percentage * this.imagePaddingWhenSticky;
                    const padding: number = (1 - percentage) * this.elementPadding;

                    this.headerContainer.style.transition = "none";
                    this.headerContainer.style.transform = "translateY(0)";
                    this.headerContainer.style.paddingTop = `${padding}px`;
                    this.headerContainer.style.paddingBottom = `${padding}px`;
                    this.imageElement.style.padding = `${imageElementPadding}px`;
                    this.headerContainer.classList.remove("shadow-md");
                }
            }

            this.scrollPosition = window.scrollY;
        });
    }

    hamburgerMenuOnClick(): void {
        this.$emit("hamburger-menu-clicked");
    }

    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}
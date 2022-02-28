import WaveButton from "@/Components/WaveButton/WaveButton.vue";
import Theme from "@/Models/Theme";
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
    showDropdown: boolean | null = null;
    isDarkMode: boolean = false;
    themes: Array<Theme> = [];
    selectedTheme!: Theme;
    isLargeScreen: boolean = false;

    created(): void {
        localStorage.theme = "light";
        this.themes.push(new Theme("light", "/Images/Icons/Theme-light.svg#theme-light"), new Theme("dark", "/Images/Icons/Theme-dark.svg#theme-dark"));
        this.selectedTheme = this.themes[0];
    }

    mounted(): void {
        this.elementHeight = this.headerContainer.getHeight();
        this.elementPadding = this.headerContainer.getPaddingTop();
        this.elementTransition = this.headerContainer.getTransition();
        this.isLargeScreen = window.matchMedia("(min-width: 1024px)").matches;

        (this.$el as HTMLElement).style.height = `${this.elementHeight}px`;

        window.addEventListener("scroll", () => {
            this.headerContainer.style.transition = this.elementTransition;

            if(this.scrollPosition < window.scrollY) {
                if(window.scrollY < this.elementHeight) {
                    this.headerContainer.style.transition = "none";
                    this.headerContainer.style.transform = `translateY(-${window.scrollY}px)`;
                } else {
                    this.headerContainer.style.transform = "translateY(-100%)";
                    this.headerContainer.classList.remove(localStorage.theme === "dark" ? "bg-dark" : "bg-white");
                    this.headerContainer.classList.remove(localStorage.theme === "dark" ? "shadow-md-dark" : "shadow-md");
                }
            } else if(this.scrollPosition > window.scrollY) {
                if(window.scrollY > this.elementHeight * 2) {
                    this.imageElement.style.padding = `${this.imagePaddingWhenSticky}px`;
                    this.headerContainer.style.paddingTop = "0";
                    this.headerContainer.style.paddingBottom = "0";
                    this.headerContainer.style.transform = "translateY(0)";
                    this.headerContainer.classList.add(localStorage.theme === "dark" ? "bg-dark" : "bg-white");
                    this.headerContainer.classList.add(localStorage.theme === "dark" ? "shadow-md-dark" : "shadow-md");
                } else if(window.scrollY <= this.elementHeight && window.scrollY >= 0) {
                    const percentage: number = (window.scrollY / this.elementHeight);
                    const imageElementPadding: number = percentage * this.imagePaddingWhenSticky;
                    const padding: number = (1 - percentage) * this.elementPadding;

                    this.headerContainer.style.transition = "none";
                    this.headerContainer.style.transform = "translateY(0)";
                    this.headerContainer.style.paddingTop = `${padding}px`;
                    this.headerContainer.style.paddingBottom = `${padding}px`;
                    this.imageElement.style.padding = `${imageElementPadding}px`;
                    this.headerContainer.classList.remove(localStorage.theme === "dark" ? "shadow-md-dark" : "shadow-md");

                    window.scrollY === 0 ?
                        this.headerContainer.classList.remove(localStorage.theme === "dark" ? "bg-dark" : "bg-white") :
                        this.headerContainer.classList.add(localStorage.theme === "dark" ? "bg-dark" : "bg-white");
                }
            }

            this.scrollPosition = window.scrollY;
        });

        window.addEventListener("resize", () => {
            this.isLargeScreen = window.matchMedia("(min-width: 1024px)").matches;
        });
    }

    hamburgerMenuOnClick(): void {
        this.$emit("hamburger-menu-clicked");
    }

    changeTheme(theme: Theme): void {
        const html: HTMLHtmlElement = document.querySelector("html") as HTMLHtmlElement;
        html.classList.add(theme.name);

        if(localStorage.theme && localStorage.theme != theme.name) {
            html.classList.remove(localStorage.theme);
        }

        this.selectedTheme = theme;
        localStorage.theme = theme.name;
        this.isDarkMode = theme.name === "dark";

        this.changeHeaderColor();
        this.changeHeaderShadow();
    }

    changeHeaderColor(): void {
        if(![this.headerContainer.classList].some((className: DOMTokenList) => className.value.match("bg"))) {
            return;
        }

        if(localStorage.theme === "dark" && this.headerContainer.classList.contains("bg-white")) {
            this.headerContainer.classList.replace("bg-white", "bg-dark");
        } else if(localStorage.theme === "light" && this.headerContainer.classList.contains("bg-dark")) {
            this.headerContainer.classList.replace("bg-dark", "bg-white");
        }
    }

    changeHeaderShadow(): void {
        if(![this.headerContainer.classList].some((className: DOMTokenList) => className.value.match("shadow"))) {
            return;
        }

        if(localStorage.theme === "dark" && this.headerContainer.classList.contains("shadow-md")) {
            this.headerContainer.classList.replace("shadow-md", "shadow-md-dark");
        } else if(localStorage.theme === "light" && this.headerContainer.classList.contains("shadow-md-dark")) {
            this.headerContainer.classList.replace("shadow-md-dark", "shadow-md");
        }
    }

    showDropDown(): void {
        this.showDropdown = !this.showDropdown;
    }

    hideDropDown(): void {
        this.showDropdown = false;
    }

    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}
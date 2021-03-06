import WaveButton from "@/Components/WaveButton/WaveButton.vue";
import { Options, Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Options({
    components: {
        WaveButton
    },
    emits: [
        "close-button-clicked"
    ]
})
export default class Overlay extends Vue {
    @Prop({default: false}) display!: boolean;

    overlayContainerElement!: HTMLElement;
    animateChilds: boolean = false;

    mounted(): void {
        this.overlayContainerElement = (this.$el as HTMLElement);
    }

    onClickCloseButton(): void {
        this.$emit("close-button-clicked");
    }

    onTransitionEnd(): void {
        this.animateChilds = this.display;
    }

    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}
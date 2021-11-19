import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { ArrowDirection } from "./ArrowDirections";

export default class AnimatedArrows extends Vue {
    @Prop() direction!: ArrowDirection;

    mounted(): void {
        const classes: Array<string> = this.direction.split(" ");
        (this.$el as HTMLElement).classList.add("transform", ...classes);
    }

    onClick(): void {
        (this.$el as HTMLElement).classList.add("clickAnimation");
    }

    onTransitionEnd(): void {
        (this.$el as HTMLElement).classList.remove("clickAnimation");
    }
}
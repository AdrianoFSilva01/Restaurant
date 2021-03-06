import { Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { ArrowDirection } from "./ArrowDirections";

export default class AnimatedArrows extends Vue {
    @Prop() direction!: ArrowDirection;
    @Prop({default: "bg-black"}) childClass!: string;

    mounted(): void {
        const classes: Array<string> = this.direction.split(" ");
        (this.$el as HTMLElement).classList.add("transform", ...classes);
    }

    onClick(): void {
        (this.$el as HTMLElement).classList.add("click-animation");
    }

    onTransitionEnd(): void {
        (this.$el as HTMLElement).classList.remove("click-animation");
    }
}
import { Options, Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Options({
    emits: [
        "changed-index"
    ]
})
export default class InlineList extends Vue {
    @Prop() list!: Array<string>;

    currentIndex: number = 0;

    onClick(touchEvent: TouchEvent): void {
        const containerElement: HTMLElement = this.$el as HTMLElement;
        const targetElementParent: HTMLElement = (touchEvent.target as HTMLElement).parentElement as HTMLElement;
        const index: number = Array.from(containerElement.children).indexOf(targetElementParent);

        if(index !== this.currentIndex) {
            this.currentIndex = index;

            this.$emit("changed-index", this.currentIndex);
        }
    }
}
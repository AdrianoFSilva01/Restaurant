import AnimatedArrows from "@/Components/AnimatedArrows/AnimatedArrows.vue";
import ArrowDirectionMixin from "@/Components/AnimatedArrows/ArrowDirections";
import WaveButton from "@/Components/WaveButton/WaveButton.vue";
import { mixins, Options } from "vue-class-component";

@Options({
    components: {
        AnimatedArrows,
        WaveButton
    }
})
export default class FooterView extends mixins(ArrowDirectionMixin) {
    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}
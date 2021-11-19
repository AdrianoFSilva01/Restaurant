import AnimatedArrows from "@/Components/AnimatedArrows/AnimatedArrows.vue";
import ArrowDirectionMixin from "@/Components/AnimatedArrows/ArrowDirections";
import HelloWorld from "@/Components/HelloWorld.vue";
import SliderTs from "@/Components/Slider/Slider";
import Slider from "@/Components/Slider/Slider.vue";
import { mixins, Options } from "vue-class-component";
import { Ref } from "vue-property-decorator";

@Options({
    components: {
        HelloWorld,
        Slider,
        AnimatedArrows
    }
})
export default class MainView extends mixins(ArrowDirectionMixin) {
    @Ref() slider!: SliderTs;

    sliderImages: Array<string> = [
        "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg",
        "https://images.unsplash.com/photo-1541963463532-d68292c34b19?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8&w=1000&q=80",
        "https://cdn.pixabay.com/photo/2021/08/25/20/42/field-6574455__480.jpg"
    ];
    disableSliderButtons: boolean = false;

    onSliderNextButtonClick(): void {
        this.disableSliderButtons = true;

        this.slider.nextImage();
    }

    onSliderPreviousButtonClick(): void {
        this.disableSliderButtons = true;

        this.slider.previousImage();
    }
}
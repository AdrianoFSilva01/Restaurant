import HelloWorld from "@/Components/HelloWorld.vue";
import SliderTs from "@/Components/Slider/Slider";
import Slider from "@/Components/Slider/Slider.vue";
import { Options, Vue } from "vue-class-component";
import { Ref } from "vue-property-decorator";

@Options({
    components: {
        HelloWorld,
        Slider
    }
})
export default class MainView extends Vue {
    @Ref() slider!: SliderTs;

    sliderImages: Array<string> = [
        "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg",
        "https://images.unsplash.com/photo-1541963463532-d68292c34b19?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8&w=1000&q=80",
        "https://cdn.pixabay.com/photo/2021/08/25/20/42/field-6574455__480.jpg"
    ];
    disableSliderButton: boolean = false;

    onSliderNextButtonClick(): void {
        this.disableSliderButton = true;

        this.slider.nextImage();
    }

    onSliderPreviousButtonClick(): void {
        this.disableSliderButton = true;

        this.slider.previousImage();
    }
}
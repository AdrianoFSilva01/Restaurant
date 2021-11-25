import AnimatedArrows from "@/Components/AnimatedArrows/AnimatedArrows.vue";
import ArrowDirectionMixin from "@/Components/AnimatedArrows/ArrowDirections";
import Carousel from "@/Components/Carousel/Carousel.vue";
import SliderTs from "@/Components/Slider/Slider";
import Slider from "@/Components/Slider/Slider.vue";
import { Axios, AxiosResponse } from "axios";
import { mixins, Options } from "vue-class-component";
import { Inject, Ref } from "vue-property-decorator";

@Options({
    components: {
        Slider,
        AnimatedArrows,
        Carousel
    }
})
export default class MainView extends mixins(ArrowDirectionMixin) {
    @Ref() slider!: SliderTs;
    @Inject() axios!: Axios;

    Categories: Array<Category> | null = null;

    created(): void {
        this.axios.get<Array<Category>>("Category/weekly")
            .then((response: AxiosResponse<Array<Category>>) => {
                this.Categories = response.data;
            });
    }

    sliderImages: Array<string> = [
        "https://tul.imgix.net/content/article/sasso_1.jpg?auto=format,compress&w=520&h=390&fit=crop",
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

class Category {
    id!: number;
    name!: string;
    imageUrl!: string;
    catalogs!: Array<Catalog>;
}

class Catalog {
    id!: number;
    name!: string;
    imageUrl!: string;
    haveIngredients!: boolean;
    catalogInfos!: Array<CatalogInfo>;
}

class CatalogInfo {
    size!: string;
    price!: number;
    description!: string;
}
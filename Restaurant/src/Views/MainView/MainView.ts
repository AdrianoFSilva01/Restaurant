import AnimatedArrows from "@/Components/AnimatedArrows/AnimatedArrows.vue";
import ArrowDirectionMixin from "@/Components/AnimatedArrows/ArrowDirections";
import CarouselTs from "@/Components/Carousel/Carousel";
import Carousel from "@/Components/Carousel/Carousel.vue";
import InlineListTs from "@/Components/InlineList/InlineList";
import InlineList from "@/Components/InlineList/InlineList.vue";
import SliderTs from "@/Components/Slider/Slider";
import Slider from "@/Components/Slider/Slider.vue";
import { Axios, AxiosResponse } from "axios";
import { mixins, Options } from "vue-class-component";
import { Inject, Ref, Watch } from "vue-property-decorator";

@Options({
    components: {
        Slider,
        AnimatedArrows,
        Carousel,
        InlineList
    }
})
export default class MainView extends mixins(ArrowDirectionMixin) {
    @Ref() slider!: SliderTs;
    @Ref() dessertsSlider!: SliderTs;
    @Ref() carousel!: CarouselTs;
    @Ref() dessertsCarousel!: CarouselTs;
    @Ref() inlineList!: InlineListTs;
    @Inject() axios!: Axios;

    Categories: Array<Category> | null = null;
    CategoriesName: Array<string> = [];

    sliderImages: Array<string> = [
        "https://tul.imgix.net/content/article/sasso_1.jpg?auto=format,compress&w=520&h=390&fit=crop",
        "https://images.unsplash.com/photo-1541963463532-d68292c34b19?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8&w=1000&q=80",
        "https://cdn.pixabay.com/photo/2021/08/25/20/42/field-6574455__480.jpg"
    ];
    sliderIndex: number = 0;

    carouselIndex: number = 0;
    dessertsCarouselIndex: number = 0;

    get catalogName(): Array<string> {
        if(this.Categories) {
            return this.Categories.flatMap((c: Category) => c.catalogs).flatMap((c: Catalog) => c.name);
        }
        return [];
    }

    get catalogImage(): Array<string> {
        if(this.Categories) {
            return this.Categories.flatMap((c: Category) => c.catalogs).flatMap((c: Catalog) => c.imageUrl);
        }
        return [];
    }

    @Watch(nameof((mainView: MainView) => mainView.carouselIndex))
    onCarouselIndexChange(): void {
        this.changeInlineListIndex();
    }

    created(): void {
        this.axios.get<Array<Category>>("Category/weekly")
            .then((response: AxiosResponse<Array<Category>>) => {
                this.Categories = response.data;

                for(const category of this.Categories) {
                    this.CategoriesName.push(category.name);
                }
            });
    }

    onSliderButtonClick(sliderIndex: number): void {
        let index: number = sliderIndex;

        if(sliderIndex < 0) {
            index = this.sliderImages.length - 1;
        } else if(sliderIndex > this.sliderImages.length - 1) {
            index = 0;
        }

        this.sliderIndex = index;
    }

    InlineListChangedIndex(index: number): void {
        const elementOfIndex: HTMLElement = document.getElementById(`carouselCategory${index}`) as HTMLElement;
        this.carousel.translateThroughElement(elementOfIndex);
    }

    changeInlineListIndex(): void {
        const carouselElementOfIndex: HTMLElement = document.getElementsByClassName("carouselItem")[this.carouselIndex] as HTMLElement;
        const idOfElement: number = Number(carouselElementOfIndex.id.split("carouselCategory")[1]);

        if(this.inlineList.currentIndex !== idOfElement) {
            this.inlineList.currentIndex = idOfElement;
        }
    }

    onDessertButtonClick(dessertsCarouselIndex: number): void {
        let index: number = dessertsCarouselIndex;

        if(dessertsCarouselIndex < 0) {
            index = this.catalogName.length - 1;
        } else if(dessertsCarouselIndex > this.catalogName.length - 1) {
            index = 0;
        }

        this.dessertsCarouselIndex = index;
    }

    sliderTouchStart(): void {
        this.dessertsCarousel.stopTransition();
    }

    sliderTouchMoving(percentage: number, minIndex: number, maxIndex: number): void {
        this.dessertsCarousel.translateThroughPositionPercentage(percentage / this.catalogName.length, minIndex, maxIndex);
    }

    sliderTouchEnded(): void {
        this.dessertsCarousel.alignTranslate();
    }

    dessertsCarouselTouchStart(): void {
        this.dessertsSlider.stopTransition();
    }

    dessertsCarouselTouchMoving(percentage: number ): void {
        this.dessertsSlider.changeImageThroughPercentage(percentage);
    }

    dessertsCarouselTouchEnded(): void {
        this.dessertsSlider.adjustElementsThroughIndex();
    }

    potato(number: number): number {
        return number;
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
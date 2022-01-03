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
    DessertCatalogs: Array<Category> | null = null;
    CategoriesName: Array<string> = [];

    sliderImages: Array<string> = [
        "https://media.architecturaldigest.com/photos/5cf690979a329c9785220c5b/3:2/w_2798,h_1865,c_limit/(c)%20Kevin%20Scott%20-%20Canlis%20(5).jpg",
        "https://free4kwallpapers.com/uploads/originals/2015/07/25/restaurants-lighting-design.jpg",
        "https://cdnb.artstation.com/p/assets/images/images/016/385/443/4k/pasquale-scionti-restaurant-lumion-00-00-24-06.jpg?1551959913",
        "https://images.squarespace-cdn.com/content/v1/55488e38e4b0f2df4ca91881/1463050370917-JG9JJGN5LGTDDJIRKVM3/GALAXY_BAR_TERRACE_credit+hilton.jpg?format=2500w"
    ];
    sliderIndex: number = 0;

    carouselIndex: number = 0;
    dessertsCarouselIndex: number = 0;

    get catalogName(): Array<string> {
        if(this.DessertCatalogs) {
            return this.DessertCatalogs.flatMap((c: Category) => c.name);
        }
        return [];
    }

    get catalogImage(): Array<string> {
        if(this.DessertCatalogs) {
            return this.DessertCatalogs.flatMap((c: Category) => c.imageUrl);
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

        this.axios.get<Array<Category>>("Category/5/detail")
            .then((response: AxiosResponse<Array<Category>>) => {
                this.DessertCatalogs = response.data;
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

    sliderInteractionStart(): void {
        this.dessertsCarousel.stopTransition();
    }

    sliderInteractionMoving(percentage: number, minIndex: number, maxIndex: number): void {
        this.dessertsCarousel.translateThroughPositionPercentage(percentage / this.catalogName.length, minIndex, maxIndex);
    }

    sliderInteractionEnded(): void {
        this.dessertsCarousel.alignTranslate();
    }

    dessertsCarouselInteractionStart(): void {
        this.dessertsSlider.stopTransition();
    }

    dessertsCarouselInteractionMoving(percentage: number ): void {
        this.dessertsSlider.changeImageThroughPercentage(percentage);
    }

    dessertsCarouselInteractionEnded(): void {
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
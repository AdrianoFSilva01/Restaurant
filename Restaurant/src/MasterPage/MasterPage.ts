import Footer from "@/Views/FooterView/FooterView.vue";
import Header from "@/Views/HeaderView/Header.vue";
import MainView from "@/Views/MainView/MainView.vue";
import Overlay from "@/Views/OverlayView/Overlay.vue";
import { Options, Vue } from "vue-class-component";
import { Watch } from "vue-property-decorator";

@Options({
    components: {
        MainView,
        Header,
        Overlay,
        Footer
    }
})

export default class MasterPage extends Vue {
    displayOverlay: boolean = false;

    @Watch(nameof((masterPage: MasterPage) => masterPage.displayOverlay))
    onCurrentIndexChange(): void {
        if(this.displayOverlay) {
            document.getElementsByTagName("html")[0].style.overflow = "hidden";
        } else {
            document.getElementsByTagName("html")[0].style.overflow = "visible";
        }
    }
}
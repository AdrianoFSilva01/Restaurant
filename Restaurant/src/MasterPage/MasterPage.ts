import Footer from "@/Views/FooterView/FooterView.vue";
import Header from "@/Views/HeaderView/Header.vue";
import MainView from "@/Views/MainView/MainView.vue";
import Overlay from "@/Views/OverlayView/Overlay.vue";
import { Options, Vue } from "vue-class-component";

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
}
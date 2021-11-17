import HelloWorld from "@/Components/HelloWorld.vue";
import Header from "@/Views/HeaderView/Header.vue";
import Overlay from "@/Views/OverlayView/Overlay.vue";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
        HelloWorld,
        Header,
        Overlay
    }
})
export default class MasterPage extends Vue {
    displayOverlay: boolean = false;
}
import { Options, Vue } from "vue-class-component";

@Options({
    emits: [
        "hamburger-menu-clicked"
    ]
})
export default class Header extends Vue {
    hamburgerMenuOnClick(): void {
        this.$emit("hamburger-menu-clicked");
    }
}
import { Vue } from "vue-class-component";

export enum ArrowDirection {
    left = "scale-x-100",
    right = "-scale-x-100",
    top = "rotate-90",
    bottom = "-scale-x-100 rotate-90"
}

export default class ArrowDirectionMixin extends Vue {
    get ArrowDirection(): typeof ArrowDirection {
        return ArrowDirection;
    };
}
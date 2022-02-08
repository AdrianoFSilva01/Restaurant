import { nextTick } from "vue";
import { Vue } from "vue-class-component";
import { Prop, Ref } from "vue-property-decorator";

export default class ContextMenu extends Vue {
    @Ref() contextMenu!: HTMLElement;
    @Prop({default: false}) positionThroughElement!: boolean;
    @Prop({default: false}) openToLeft!: boolean;

    hidden: boolean = false;
    current: HTMLElement | undefined = undefined;

    mounted(): void {
        window.addEventListener("scroll", () => {
            this.hide();
        });
    }

    toggle(event: Event): void {
        if((event.target === this.current || this.current?.contains(event.target as HTMLElement) || (event.target as HTMLElement).contains(this.current as HTMLElement)) && !this.hidden) {
            this.contextMenu.classList.add("hidden");
            return;
        }

        this.contextMenu.classList.remove("hidden");

        if(this.positionThroughElement) {
            const top: number = (event.target as HTMLElement).getBoundingClientRect().top +
                (event.target as HTMLElement).getBoundingClientRect().height -
                (event.target as HTMLElement).getPaddingTop();
            const left: number = (event.target as HTMLElement).getBoundingClientRect().left + (event.target as HTMLElement).getPaddingTop();
            nextTick(() => {
                this.openToLeft ?
                    this.setupSidesToLeft(left, top, event) :
                    this.setupSidesToRigth(left, top, event);
            });
        } else {
            this.openToLeft ?
                this.setupSidesToLeft((event as MouseEvent).clientX, (event as MouseEvent).clientY, undefined) :
                this.setupSidesToRigth((event as MouseEvent).clientX, (event as MouseEvent).clientY, undefined);
        }

        this.current = event.target as HTMLElement;
    }

    hide(): void {
        if(this.contextMenu) {
            this.hidden = this.contextMenu.classList.contains("hidden");
            this.contextMenu.classList.add("hidden");
        }
    }

    setupSidesToRigth(clientX: number, clientY: number, event: Event | undefined): void {
        let left: number | undefined;
        let right: number | undefined;
        let top: number | undefined;
        let bottom: number | undefined;

        let rightSide: boolean = false;

        if(clientX > window.innerWidth - this.contextMenu.offsetWidth) {
            right = 0;
            rightSide = true;
        } else {
            left = clientX;
        }

        if(clientY + this.contextMenu.offsetHeight > window.innerHeight) {
            if (rightSide) {
                top = (clientY - this.contextMenu.offsetHeight) + window.scrollY;
            } else {
                if((clientY - (event?.target as HTMLElement).getHeight()) < this.contextMenu.offsetHeight) {
                    left = clientX + (event?.target as HTMLElement).getWidth() + this.contextMenu.getWidth() > window.innerWidth ?
                        clientX - this.contextMenu.getWidth() :
                        clientX + (event?.target as HTMLElement).getWidth();
                    top = clientY / 2;
                } else {
                    this.positionThroughElement ? top = (clientY - (event?.target as HTMLElement).getHeight() - this.contextMenu.offsetHeight) + window.scrollY :
                        bottom = 0;
                }
            }
        } else {
            top = clientY + window.scrollY;
        }

        this.setSides(left, right, top, bottom, "px");
    }

    setupSidesToLeft(clientX: number, clientY: number, event: Event | undefined): void {
        let left: number | undefined;
        let right: number | undefined;
        let top: number | undefined;
        let bottom: number | undefined;

        const containerWidth: number = this.positionThroughElement ?
            this.contextMenu.getWidth() - (event?.target as HTMLElement).getWidth() :
            this.contextMenu.getWidth();

        if(clientX < containerWidth) {
            left = clientX;
        } else {
            left = clientX - containerWidth;
        }

        if(clientY + this.contextMenu.offsetHeight > window.innerHeight) {
            if((clientY - (event?.target as HTMLElement).getHeight()) < this.contextMenu.offsetHeight) {
                left = clientX < this.contextMenu.getWidth() ?
                    clientX + (event?.target as HTMLElement).getWidth() :
                    clientX - this.contextMenu.getWidth();
                top = clientY / 2;
            } else {
                this.positionThroughElement ? top = (clientY - (event?.target as HTMLElement).getHeight() - this.contextMenu.offsetHeight) + window.scrollY :
                    bottom = 0;
            }
        } else {
            top = clientY + window.scrollY;
        }

        this.setSides(left, right, top, bottom, "px");
    }

    setSides(left: number | undefined, right: number | undefined, top: number | undefined, bottom: number | undefined, unit: string): void {
        this.contextMenu.style.left = this.getSideValue(left, unit);
        this.contextMenu.style.right = this.getSideValue(right, unit);
        this.contextMenu.style.top = this.getSideValue(top, unit);
        this.contextMenu.style.bottom = this.getSideValue(bottom, unit);
    }

    getSideValue(value: number | undefined, unit: string): string {
        if (value !== undefined) {
            return `${value}${unit}`;
        }

        return "unset";
    }
}
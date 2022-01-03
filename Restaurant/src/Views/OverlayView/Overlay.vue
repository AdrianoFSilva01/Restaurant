<template>
    <div class="overlay" :class="{ over : animateChilds }" @transitionend.self="onTransitionEnd" v-delayed-childs="[0.1, 'delayedTransition', 'animate']">
        <div class="closeButton" @click="onClickCloseButton">
            <hr />
            <hr />
        </div>
        <div class="space-y-8 lg:pr-4">
            <nav class="navigationList">
                <ul class="delayedTransition">
                    Home
                </ul>
                <ul class="delayedTransition">
                    Order Now
                </ul>
            </nav>
            <hr class="delayedTransition" />
            <button class="orderButton delayedTransition">
                Order Now
            </button>
        </div>
        <hr class="delayedTransition lg:hidden" />
        <div class="contactInfo">
            <ul class="contactList">
                <li class="delayedTransition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Phone.svg#phone" />
                    </svg>
                    <p>
                        931 662 001
                    </p>
                </li>
                <li class="delayedTransition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Mail.svg#mail" />
                    </svg>
                    <p>
                        info@madeira.pt
                    </p>
                </li>
            </ul>
            <hr class="delayedTransition my-8 lg:hidden"/>
            <ul class="contactList">
                <li class="delayedTransition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Facebook.svg#facebook" />
                    </svg>
                    <p>
                        Facebook
                    </p>
                </li>
                <li class="delayedTransition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Instagram.svg#instagram" />
                    </svg>
                    <p>
                        Instagram
                    </p>
                </li>
            </ul>
        </div>
        <hr class="delayedTransition lg:hidden" />
        <div class="termsPrivacy">
            <div class="delayedTransition">
                Terms
            </div>
            <div class="delayedTransition">
                Privacy
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Overlay.ts" />

<style scoped lang="postcss" src="./Styles/Overlay.pcss" />
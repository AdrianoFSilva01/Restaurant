<template>
    <div class="overlay" :class="{ over : animateChilds }" @transitionend.self="onTransitionEnd" v-delayed-childs="[0.1, 'delayed-transition', 'animate']">
        <div class="close-button" @click="onClickCloseButton">
            <hr />
            <hr />
        </div>
        <div class="space-y-8 lg:pr-4">
            <nav class="navigation-list">
                <ul class="delayed-transition" @click="onClickCloseButton">
                    <router-link to="/" @click="scrollToTop">
                        Home
                    </router-link>
                </ul>
                <ul class="delayed-transition" @click="onClickCloseButton">
                    <router-link to="/order" @click="scrollToTop">
                        Order Now
                    </router-link>
                </ul>
            </nav>
            <hr class="delayed-transition" />
            <div class="delayed-transition">
                <router-link to="/order" @click="scrollToTop">
                    <WaveButton class="order-button" background-color="white" text-color="white" text-hover-color="#d6a159" buttontext="Order Now" @click="onClickCloseButton" />
                </router-link>
            </div>
        </div>
        <hr class="delayed-transition lg:hidden" />
        <div class="contact-info">
            <ul class="contact-list">
                <li class="delayed-transition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Phone.svg#phone" />
                    </svg>
                    <p>
                        931 662 001
                    </p>
                </li>
                <li class="delayed-transition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Mail.svg#mail" />
                    </svg>
                    <p>
                        info@madeira.pt
                    </p>
                </li>
            </ul>
            <hr class="delayed-transition my-8 lg:hidden"/>
            <ul class="contact-list">
                <li class="delayed-transition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Facebook.svg#facebook" />
                    </svg>
                    <p>
                        Facebook
                    </p>
                </li>
                <li class="delayed-transition">
                    <svg viewBox="0 0 100 100">
                        <use href="@/Assets/Images/Instagram.svg#instagram" />
                    </svg>
                    <p>
                        Instagram
                    </p>
                </li>
            </ul>
        </div>
        <hr class="delayed-transition lg:hidden" />
        <div class="terms-privacy">
            <div class="delayed-transition">
                Terms
            </div>
            <div class="delayed-transition">
                Privacy
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Overlay.ts" />

<style scoped lang="scss" src="./Styles/Overlay.scss" />
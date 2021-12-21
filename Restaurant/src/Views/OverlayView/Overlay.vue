<template>
    <div class="overlay" :class="{ over : animateChilds }" @transitionend.self="onTransitionEnd" v-delayed-childs="[0.1, 'delayedTransition', 'animate']">
        <div class="closeButton" @click="onClickCloseButton">
            <hr />
            <hr />
        </div>
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
        <hr class="delayedTransition" />
        <div class="contactList">
            <ul class="delayedTransition">
                <svg viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Phone.svg#phone" />
                </svg>
                <p>
                    931 662 001
                </p>
            </ul>
            <ul class="delayedTransition">
                <svg viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Mail.svg#mail" />
                </svg>
                <p>
                    info@madeira.pt
                </p>
            </ul>
        </div>
        <hr class="delayedTransition" />
        <div class="socialList">
            <ul class="delayedTransition">
                <svg viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Facebook.svg#facebook" />
                </svg>
                <p>
                    Facebook
                </p>
            </ul>
            <ul class="delayedTransition">
                <svg viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Instagram.svg#instagram" />
                </svg>
                <p>
                    Instagram
                </p>
            </ul>
        </div>
    </div>
</template>

<script lang="ts" src="./Overlay.ts" />

<style scoped lang="postcss" src="./Overlay.pcss" />
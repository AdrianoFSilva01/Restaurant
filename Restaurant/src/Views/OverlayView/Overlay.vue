<template>
    <div class="overlay" :class="{ over : animateChilds }" @transitionend.self="onTransitionEnd" v-delayed-childs="[0.1, 'delayedTransition']">
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
                <img src="@/Assets/Images/Phone.svg"/>
                <p>
                    931 662 001
                </p>
            </ul>
            <ul class="delayedTransition">
                <img src="@/Assets/Images/Mail.svg"/>
                <p>
                    info@madeira.pt
                </p>
            </ul>
        </div>
        <hr class="delayedTransition" />
        <div class="socialList">
            <ul class="delayedTransition">
                <div>
                    <img src="@/Assets/Images/Facebook.svg" />
                </div>
                <p>
                    Facebook
                </p>
            </ul>
            <ul class="delayedTransition">
                <div>
                    <img src="@/Assets/Images/Instagram.svg" />
                </div>
                <p>
                    Instagram
                </p>
            </ul>
        </div>
    </div>
</template>

<script lang="ts" src="./Overlay.ts" />

<style scoped lang="postcss" src="./Overlay.pcss" />
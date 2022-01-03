<template>
    <div class="headerContainer">
        <nav class="navigationText">
            Order
            <br />
            Now
        </nav>
        <div ref="imageElement" class="navigationImage">
            <img src="@/Assets/Images/logo.svg" />
        </div>
        <nav class="navigationIcon">
            <div class="hamburgerMenu" @click="hamburgerMenuOnClick">
                <hr />
                <hr />
                <hr />
            </div>
            <a class="navigationPages">
                Order
            </a>
        </nav>
        <div class="navigationButton">
            <button class="button">
                ORDER NOW
            </button>
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style scoped lang="postcss" src="./Styles/Header.pcss" />

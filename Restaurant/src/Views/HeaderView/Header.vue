<template>
    <div class="headerContainer">
        <div class="navigationText">
            Order
            <br />
            Now
        </div>
        <div ref="imageElement" class="navigationImage">
            <img src="@/Assets/Images/logo.svg" />
        </div>
        <div class="navigationIcon">
            <div class="hamburgerMenu" @click="hamburgerMenuOnClick">
                <hr />
                <hr />
                <hr />
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style scoped lang="postcss" src="./Header.pcss" />

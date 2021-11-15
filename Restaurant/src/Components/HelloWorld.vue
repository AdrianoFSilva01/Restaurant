<template>
    <div>
        <div>
            <table>
                <thead>
                    <tr>
                        <th>
                            Date
                        </th>
                        <th>
                            Temp. (C)
                        </th>
                        <th>
                            Temp. (F)
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="forecast in Categories" :key="forecast.id">
                        <td>
                            {{ forecast.id }}
                        </td>
                        <td>
                            {{ forecast.name }}
                        </td>
                        <td>
                            {{ forecast.imageUrl }}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { Axios, AxiosResponse } from "axios";
import { Vue } from "vue-class-component";
import { Inject } from "vue-property-decorator";

class Category {
    Id!: number;
    Name!: string;
    ImageUrl!: string;
}

export default class HelloWorld extends Vue {
    @Inject() axios!: Axios;
    Categories: Array<Category> = [];

    created(): void {
        this.axios.get<Array<Category>>("Category")
            .then((response: AxiosResponse<Array<Category>>) => {
                this.Categories = response.data;
            });
    }
}
</script>

<template>
  <v-container>
    <v-raw><v-img :src="url1"/></v-raw>
    <v-raw>
      <v-btn @click="onClick()"
        text
      >
        <span class="mr-2">Latest Release</span>
        <v-icon>mdi-open-in-new</v-icon>
      </v-btn>
    </v-raw>
  </v-container>
</template>

<script>

  export default {
    name: 'TestView',
    data:() => ( 
    {
        url1: "https://picsum.photos/id/11/10/6",
        url2: "https://upload.wikimedia.org/wikipedia/commons/9/9c/Bundesarchiv_Bild_183-R69173%2C_M%C3%BCnchener_Abkommen%2C_Staatschefs.jpg",
        url3: "https://picsum.photos/id/11/10/6",
    }),
    methods: 
    {
        onClick: function()
        {
            if (this.url1 == this.url2) 
            {
                this.url1 = this.url3
            }
            else{this.url1 = this.url2}
        }
    }

  }
</script>

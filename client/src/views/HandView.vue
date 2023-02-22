<template>
  <v-container>
    <v-raw>YOUR HAND:</v-raw>
    <v-raw>
        <v-col v-for="card in hand" :key="card">          
            <v-img :src="getCardImage(card)"/>
        </v-col>
    </v-raw>
  </v-container>
</template>

<script>

  export default {
    name: 'HandView',
    data:() => ( 
    {
        hand: ["XS", "KH", "AD", "QC"]
    }),
    methods: 
    {
        getCardImage: function(code)
        {
            let s = code[1]
            let n = code[0]
            return `./assets/cards/${this.getPrefix(n)}_of_${this.getSuffix(s)}.svg`
        },
        getPrefix: function(n)
        {
            if (n == 'X') return '10'
            if (n == 'J') return 'jack'
            if (n == 'Q') return 'queen'
            if (n == 'A') return 'ace'
            if (n == 'K') return 'king'
            return n
        },
        getSuffix: function(n)
        {
            if (n == 'S') return 'spades'
            if (n == 'H') return 'hearts'
            if (n == 'D') return 'diamonds'
            if (n == 'C') return 'clubs'
        },

    }

  }
</script>

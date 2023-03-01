<template>
  <v-container>
      <v-row justify="center"><v-col cols="2" v-for="card in hand" :key="card">          
        <v-img :src="getCardImage(card)"/></v-col>
      </v-row>
      <v-row>
        <h1>YOUR SCORE: {{getHandScore()}}</h1>
        <v-spacer/>
        <v-btn class="ma-2" @click="hit()">HIT</v-btn>
        <v-btn class="ma-2" @click="stand()">STAND</v-btn>
      </v-row>
  </v-container>
</template>

<script>

  export default {
    name: 'HandView',
    data:() => ( 
    {
        hand: ["XS", "AD"]
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
        hit: function()
        {
          let n = Math.floor(Math.random() * 13);
          let m = Math.floor(Math.random() * 4);
          let symbols = "A23456789XJQK"
          let seeds = "SHDC"
          let card = `${symbols[n]}${seeds[m]}`
          this.hand.push(card)
        },
        stand: function()
        {
          
        },
        getCardScore: function(card)
        {
          let symbol = card[0]
          if (symbol == 'X') return 10
          if (symbol == 'J') return 10
          if (symbol == 'Q') return 10
          if (symbol == 'A') return 1
          if (symbol == 'K') return 10
          return symbol.charCodeAt(0) - 48;
        },
        getHandScore: function()
        {
          let score = 0
          let hasace = false
          for (let card of this.hand)
          {
            let cardscore = this.getCardScore(card)
            score = score + cardscore
            hasace = hasace || (cardscore == 1)
          }
          if (score > 21) return 0
          if (hasace && score <= 11) return score + 10
          return score
        },

    }

  }
</script>

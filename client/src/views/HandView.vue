<template>
  <v-container>
      <v-row><h1>YOUR CARDS:</h1></v-row>
      <v-row><h2>TOT VALUE: {{getPlayerScore()}} </h2></v-row>
      <v-row justify="center"><v-col cols="2" v-for="card in hand" :key="card">          
        <v-img :src="getCardImage(card)"/></v-col>
      </v-row>
      <v-row>
        <v-spacer/>
        <v-btn rounded="pill" color="red" class="ma-2" :disabled="!canPlay" @click="hit()"> <v-icon>mdi-cards</v-icon> &nbsp;HIT </v-btn>
        <v-btn rounded="pill" color="green" class="ma-2" :disabled="!canPlay" @click="stand()"> <v-icon>mdi-hand-back-left</v-icon> &nbsp;STAND </v-btn>
        <v-btn rounded="pill" color="yellow" class="ma-2" @click="reload()"> <v-icon>mdi-reload</v-icon> &nbsp;RESTART </v-btn>
      </v-row>
      <v-row><h1>DEALER CARDS:</h1></v-row>
      <v-row><h2>TOT VALUE: {{dealerScore}} </h2></v-row>
      <v-row justify="center"><v-col cols="2" v-for="dcard in dealerhand" :key="dcard">          
        <v-img :src="getCardImage(dcard)"/></v-col>
      </v-row>

      <div class="container" v-show="lost">
        <span class="center lost">
          YOU LOST
        </span>
      </div>
        <div class="container" v-show="won">
        <span class="center won">
          YOU WON
        </span>
      </div>
      <div class="container" v-show="tie">
        <span class="center tie">
          TIE
        </span>
      </div>

  </v-container>
</template>

<script>
  import Axios from "axios";

  async function generate() {
    let response = await Axios.get('api/random-number')
    let k = response.data % 56
    console.log(response) 
    let n = k % 13
    let m = k % 4
    let symbols = "A23456789XJQK"
    let seeds = "SHDC"
    let card = `${symbols[n]}${seeds[m]}`
    return card
  }

  export default {
    name: 'HandView',
    data: () => (
    {
        hand: [],
        dealerhand: []
    }),

    computed:
    {
      canPlay()
      {
        return (this.dealerhand[0] == "BB") && (this.getPlayerScore() > 0)
      },
      dealerPlay()
      {
        let score = this.getDealerScore()
        return (!this.canPlay) && (score > 0) && (score < 17) 
      },
      dealerScore()
      {
        if (this.dealerhand[0] != "BB") return this.getDealerScore()
        else return "?"
      },
      gameover()
      {
        let score = this.getDealerScore()
        return (this.dealerhand[0] != "BB") && ((score == 0) || (score >= 17))
      },
      lost()
      {
        return this.gameover && (this.getPlayerScore() < this.getDealerScore())
      },
      won()
      {
        return this.gameover && (this.getPlayerScore() > this.getDealerScore())
      },
      tie()
      {
        return this.gameover && (this.getPlayerScore() == this.getDealerScore())
      }
    },

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
            if (n == 'B') return 'blank'
            return n
        },
        getSuffix: function(n)
        {
            if (n == 'S') return 'spades'
            if (n == 'H') return 'hearts'
            if (n == 'D') return 'diamonds'
            if (n == 'C') return 'clubs'
            if (n == 'B') return 'blank'
        },
        hit: async function()
        {
          let card = await generate()
          this.hand.push(card)
        },
        stand: async function()
        {
          let dcard = await generate()
          this.dealerhand[0] =  dcard
          this.dealerhand = this.dealerhand.map(x => x)
          this.conditionallyPlay()
        },
        reload: function()
        {
         this.$router.go() 
        },
        getCardScore: function(card)
        {
          let symbol = card[0]
          if (symbol == 'X') return 10
          if (symbol == 'J') return 10
          if (symbol == 'Q') return 10
          if (symbol == 'A') return 1
          if (symbol == 'K') return 10
          if (symbol == 'B') return 0
          return symbol.charCodeAt(0) - 48
        },
        getHandScore: function(hand)
        {
          let score = 0
          let hasace = false
          for (let card of hand)
          {
            let cardscore = this.getCardScore(card)
            score = score + cardscore
            hasace = hasace || (cardscore == 1)
          }
          if (score > 21) return 0
          if (hasace && score <= 11) return score + 10
          return score
        },
        getDealerScore: function()
        {
          return this.getHandScore(this.dealerhand)
        },
        getPlayerScore: function()
        {
          return this.getHandScore(this.hand)
        },
        dealerHit: async function()
        {
          let card = await generate()
          this.dealerhand.push(card)
          this.conditionallyPlay()
        },
        conditionallyPlay: function()
        {
          if (this.dealerPlay) 
          {
            setTimeout( () => { this.dealerHit() }, 1000)
          }
        }
    },
    async created()
    {
      this.hand = [ await generate(), await generate()]
      this.dealerhand = ["BB", await generate()]
    }
  }
</script>

<style scoped>

.container 
  {
    position: relative;
  }

  .center {
    position: fixed;
    top: 40%;
    left: 35%;
    text-align: center;
    font-size: 18px;
  }
  .lost
  {
    width: 30vw;
    height: 30vh;
    background-color: red;
    border: 2px solid black;
    padding: 5px;
    margin: 15px;
  } 

  .won
  {
    width: 30vw;
    height: 30vh;
    background-color: green;
    border: 2px solid black;
    padding: 5px;
    margin: 15px;
  } 

  .tie
  {
    width: 30vw;
    height: 30vh;
    background-color: gold;
    border: 2px solid black;
    padding: 5px;
    margin: 15px;
  } 

</style>

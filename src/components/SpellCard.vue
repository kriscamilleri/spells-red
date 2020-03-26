<template>
  <div>
    <b-card
      img-top
      v-b-toggle="'descSpell' + spell.id"
      tag="article"
      class="mx-auto m-1 spell-card shadow text-left"
      :border-variant="classColor"
      header-bg-variant="primary"
      header-text-variant="white"
      v-b-modal="'descSpell'+spell.id"
    >
      <h4
        class="card-title text-primary"
        data-toggle="collapse"
        aria-controls="collapseOne"
      >{{spell.name}}</h4>
      <h6 class="card-subtitle mb-2 text-muted" v-html="formattedSubtitle(spell)"></h6>
      <span class="badge-shrinker align-middle">
        <b-badge variant="warning" v-if="spell.conc == true" class="mr-1">Concentration</b-badge>
        <b-badge variant="success" v-if="spell.ritual == true" class="mr-1">Ritual</b-badge>
      </span>
      <p class="card-text text-sm text-muted" align-h="start">
        <!-- <b-modal
         :id="'descSpell'+spell.id"
        class="details-text text-justify">-->
        <b-modal
          hide-footer
          size="lg"
          :id="'descSpell'+spell.id"
          class="details-text text-justify"
          :title="spell.name"
        >
          <div class="modal-stats shadow-sm">
            <span class="badge-shrinker float-right">
              <b-badge variant="warning" v-if="spell.conc == true" class="mr-1">Concentration</b-badge>
              <b-badge variant="success" v-if="spell.ritual == true" class="mr-1">Ritual</b-badge>
            </span>
            <p class="card-subtitle" v-html="formattedSubtitle(spell)"></p>
          </div>
          <div class="text-primary">Description</div>
          <span class="text-justify" v-html="formattedDescription(spell)"></span>
          <div class="text-primary">Classes</div>
          <p>{{spell.class}}</p>
          <div class="text-primary">Components</div>
          <p v-html="formattedComponents(spell)"></p>
          <div class="text-primary">School</div>
          <p>{{spell.school}}</p>
        </b-modal>
      </p>
    </b-card>
  </div>
</template>

<script>
import SpellParser from "../mixins/SpellParser.vue";

export default {
  name: "SpellCard",
  mixins: [SpellParser],
  props: {
    spell: {
      id: Number,
      name: String,
      desc: Array,
      higher: String,
      range: String,
      ritual: Boolean,
      duration: String,
      conc: Boolean,
      casting: String,
      level: String,
      school: String,
      class: Array,
      source: String,
      page: String
    }
  },
  methods: {
    expandCard() {},
    convertJsonArrayToHtml(jsonArr) {
      let text = this.splitMulti(jsonArr, ["'],['", "'], ['"]);
      text[0] = text[0].replace("['", "");
      text[text.length - 1] = text[text.length - 1].replace(/']/g, "");
      let html = `<p>${text.join("</p><p>")}</p>`;
      return html;
    },
    splitMulti(str, tokens) {
      var tempChar = tokens[0]; // We can use the first token as a temporary join character
      for (var i = 1; i < tokens.length; i++) {
        str = str.split(tokens[i]).join(tempChar);
      }
      str = str.split(tempChar);
      return str;
    }
  },
  watch: {},
  computed: {
    classColor: function() {
      return "muted";
    }
  }
};
//Stacked progress bar to denote spell class requirement
</script>
<style>
.modal-stats {
  margin-left: -16px;
  margin-right: -16px;
  padding: 15px;
  margin-top: -17px;
  margin-bottom: 15px;
  background-color: var(--light);
  border-top: 0.125rem solid var(--primary);
  border-bottom: 0.125rem solid var(--primary);
}
h6 {
  font-size: 1.15rem;
  line-height: 1.65rem;
}
.details-text {
  font-size: 1.1rem;
}

.component-text {
  font-size: 1.1rem;
}

.badge-shrinker {
  font-size: 1rem;
}
@media (max-width: 576px) {
  .spell-card.card {
    cursor: pointer;
    min-height: 160px;
    min-width: 80vw;
    max-width: 80vw;
  }
}

article.card {
  cursor: pointer;
  transform: translateZ(0);
  min-height: 190px;
  min-width: 18rem;
  max-width: 18rem;
  -webkit-transition: all 0.3s;
  transition: all 0.3s;
  border: none;
  border-radius: 0.4rem;
}
.card:hover .card-title {
  color: var(--primary-hover) !important;
  transition: all 0.3s;
}
.card:hover {
  -webkit-transform: scale(1.03);
  transform: translateZ(0);
  transform: scale(1.03);
  box-shadow: 0 0.5rem 1rem rgba(72, 72, 72, 0.25) !important;
}

.card:active {
  transform: translateZ(0);
  box-shadow: 0 0.5rem 1rem rgba(72, 72, 72, 0.25) !important;
  -webkit-transform: scale(0.98);
  transform: scale(0.98);
  outline: none;
}
.card:focus {
  outline: none;
}
/* 
.card:hover {
  transform: scale(1.05);
  transition: 0.1s ease-in-out;
}

.card:active {
  animation: 0.2s clickCard;
}

@keyframes clickCard {
  0% {
    transform: scale(1.05);
    transform: translateY(0);
  }
  100% {
    transform: scale(1);
    transform: translateY(0.125rem);
  }
  0% {
    transform: scale(1.05);
    transform: translateY(0);
  }
} */
.navbar-brand {
  margin-right: 0.5rem;
}
</style>

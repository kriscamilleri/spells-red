<template>
  <div>
    <div v-for="spell in spells" :key="spell.id">
      <div class="printable text-justify">
        <div class="info-container">
          <div class="info-subcontainer">
            <span class="info-header">
              <h3>{{spell.name}}</h3>
              <span class="float-right">
                <b-badge variant="warning" v-if="spell.conc === true">Concentration</b-badge>
                <b-badge variant="success" v-if="spell.ritual === true">Ritual</b-badge>
              </span>
              <h5 class="text-primary" v-if="spell.level">{{spell.level}} {{spell.school}}</h5>
              <h5>{{spell.class}}</h5>
            </span>
            <span class="extended-description text-justify">
              <strong v-if="spell.id" class="d-block pt-2 text-primary">Description</strong>
              <span v-html="formattedDescription(spell)"></span>
              <strong v-if="spell.id" class="text-primary">Classes</strong>
              <p>{{spell.class}}</p>
              <p v-html="formattedComponents(spell)"></p>
              <strong v-if="spell.id" class="text-primary">School</strong>
              <p>{{spell.school}}</p>
            </span>
          </div>
        </div>
        <hr />
      </div>
    </div>
  </div>
</template>
<script>
import SpellParser from "./mixins/SpellParser.vue";

export default {
  name: "Print",
  mixins: [SpellParser],
  props: {
    spells: Array
  },
  methods: {}
};
</script>
<style scoped>
.info-container {
  margin: 2rem;
  page-break-inside: avoid;
  /* margin: 20px;
  max-height: fit-content;
  -webkit-column-width: 20rem;
  -moz-column-width: 20rem;
  column-width: 20rem;
  column-count: 4;
  break-inside: avoid-page;
  -moz-column-fill: auto;
  column-fill: auto;
  -webkit-column-gap: var(--info-column-gap);
  -moz-column-gap: var(--info-column-gap);
  column-gap: var(--info-column-gap);
  -webkit-column-rule: 1px dotted var(--dark);
  -moz-column-rule: 1px dotted var(--dark);
  column-rule: 1px dotted var(--dark); */
}
.print-mode {
}
</style>
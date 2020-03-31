<template>
  <div id="app">
    <title>{{ spellBookTitle }}</title>
    <meta name="viewport" content="width=device-width, user-scalable=false;" />
    <div class="print-mode" v-if="printEnabled === true">
      <div class="row">
        <div class="col-md-12">
          <div
            class="btn btn-dark close-button btn-sm float-right print-d-none"
            @click="printEnabled = false"
          >X</div>
        </div>
      </div>
      <print :spells="filteredSpells"></print>
    </div>
    <div v-else>
      <meta name="viewport" content="width=device-width, user-scalable=false;" />
      <spell-nav-bar
        @sideBarOn="captureSideBarStatus"
        @searchText="captureSearchText"
        @printEnabled="capturePrintToggle"
        @spells="spells"
        :navTitle="navTitle"
        :sideBarOn="sideBarOn"
      ></spell-nav-bar>
      <div id="wrapper" :class="{ toggled: sideBarOn }" class="pt-2">
        <div id="sidebar-wrapper" class="nav-bg">
          <spell-filters
            @sideBarOn="captureSideBarStatus"
            :classFilters="classFilters"
            @classFilters="captureClassFilters"
            :levelFilters="levelFilters"
            @levelFilters="captureLevelFilters"
            :sourceFilters="sourceFilters"
            @sourceFilters="captureSourceFilters"
            :schoolFilters="schoolFilters"
            @schoolFilters="captureSchoolFilters"
            :ritualFilters="ritualFilters"
            @ritualFilters="captureRitualFilters"
            :concentrationFilters="concentrationFilters"
            @concentrationFilters="captureConcentrationFilters"
          ></spell-filters>
        </div>
        <!-- <add-spell></add-spell> -->
        <favorites-modal
          @spellAdded="captureAddedSpell"
          @spellRemoved="captureRemovedSpell"
          :added-spells="spellBookSpells"
          :spells="filteredSpellsNoSpellbook"
        ></favorites-modal>
        <div id="page-content-wrapper">
          <b-container fluid class>
            <b-row :class="{ 'd-none': dataLoading }" id="spellContainer" align-h="center">
              <spell-card v-for="r in cappedFilteredSpells" :spell="r" :key="r.id" class="m-2"></spell-card>
            </b-row>
            <b-row>
              <b-col class="d-flex">
                <div
                  class="btn btn-primary mt-4 mb-2 infinite-button"
                  :class="
                    cappedFilteredSpells.length < cardCount ? 'd-none' : ''
                  "
                  align="center"
                  v-on:click="toggleNextCards"
                >Load More [{{ cardCount }}/{{ filteredSpells.length }}]</div>
              </b-col>
            </b-row>
            <div v-if="dataLoading" class="text-center p-5">
              <h2>Loading data...</h2>
            </div>
            <!-- <b-pagination
              align="center"
              class="p-4 mt-4"
              v-model="currentPage"
              :total-rows="spellSize"
              :per-page="cardCount"
              aria-controls="spellContainer"
            ></b-pagination>-->
          </b-container>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
/* IDEAS
Better display names in spellbook
Add new spells
Better spell creation 
	Reset selected spells 
	Can apply to current visibility
Shadow on filters

*/

import SpellNavBar from "./components/SpellNavBar.vue";
import SpellCard from "./components/SpellCard.vue";
import Print from "./components/Print.vue";
import SpellFilters from "./components/SpellFilters.vue";
import FavoritesModal from "./components/FavoritesModal.vue";

export default {
  components: {
    SpellCard,
    // AddSpell,
    Print,
    FavoritesModal,
    SpellNavBar,
    SpellFilters
  },
  data() {
    return {
      spellBookFilter: [],
      spellBookTitle: "Spells.Red",
      navTitle: "Spells.Red",
      dataLoading: true,
      sideBarOn: false,
      searchText: "",
      spells: [],
      currentPage: 1,
      pageSize: 20,
      cardCount: 20,
      classFilters: [
        "bard",
        "cleric",
        "druid",
        "paladin",
        "ranger",
        "sorcerer",
        "warlock",
        "wizard"
      ],
      sourceFilters: ["EE", "PHB", "SCAG", "XGTE"],
      levelFilters: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
      schoolFilters: [
        "Abjuration",
        "Conjuration",
        "Divination",
        "Enchantment",
        "Evocation",
        "Illusion",
        "Necromancy",
        "Transmutation"
      ],
      concentrationFilters: ["True", "False"],
      ritualFilters: ["True", "False"],
      classes: [
        "bard",
        "cleric",
        "druid",
        "paladin",
        "ranger",
        "sorcerer",
        "warlock",
        "wizard"
      ],
      levels: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
      sources: ["EE", "PHB", "SCAG", "XGtE"],
      schools: [
        "Abjuration",
        "Conjuration",
        "Divination",
        "Enchantment",
        "Evocation",
        "Illusion",
        "Necromancy",
        "Transmutation"
      ],
      printEnabled: false,
      lastElemY: Number
    };
  },
  computed: {
    filteredSpells: function() {
      var searchText = this.searchText;
      if (this.spells) {
        let spells = this.spells;
        spells = this.filterSpellBook(spells);
        spells = this.filterSearch(spells, searchText);
        spells = this.filterClasses(spells);
        spells = this.filterLevels(spells);
        spells = this.filterSources(spells);
        spells = this.filterSchools(spells);
        spells = this.filterConcentration(spells);
        spells = this.filterRitual(spells);

        return spells;
      }
      return [];
    },
    spellBookSpells() {
      if (this.spells && this.spellBookFilter.length > 0) {
        let spells = this.spells;
        spells = this.filterSpellBook(spells);

        return spells;
      }
      return [];
    },
    filteredSpellsNoSpellbook() {
      var searchText = this.searchText;
      if (this.spells) {
        let spells = this.spells;
        spells = this.filterSearch(spells, searchText);
        spells = this.filterClasses(spells);
        spells = this.filterLevels(spells);
        spells = this.filterSources(spells);
        spells = this.filterSchools(spells);
        spells = this.filterConcentration(spells);
        spells = this.filterRitual(spells);

        return spells;
      }
      return [];
    },
    cappedFilteredSpells() {
      let spells = this.filteredSpells;
      const firstSpellid = (this.currentPage - 1) * this.cardCount;
      const lastSpellid = this.currentPage * this.cardCount;

      spells = spells.slice(firstSpellid, lastSpellid);
      return spells;
    },
    spellSize() {
      return this.spells.length;
    }
  },
  methods: {
    toggleNextCards() {
      this.cardCount += this.pageSize;
    },
    captureAddedSpell(spellId) {
      this.spellBookFilter.push(spellId);
    },
    captureRemovedSpell(spellId) {
      // this.spellBookFilter.push(spellId);
      // console.log(spellId);
      const filtered = this.spellBookFilter.filter(c => c !== spellId);
      this.spellBookFilter = [...filtered];
      // console.log(this.speconllBookFilter);
    },
    capturePrintToggle(printEnabled) {
      this.printEnabled = printEnabled;
    },
    captureSideBarStatus(sideBarOn) {
      this.sideBarOn = sideBarOn;
    },
    captureSearchText(searchText) {
      this.searchText = searchText;
    },
    captureClassFilters(value) {
      this.classFilters = value;
    },
    captureLevelFilters(value) {
      this.levelFilters = value;
    },
    captureSourceFilters(value) {
      this.sourceFilters = value;
    },
    captureSchoolFilters(value) {
      this.schoolFilters = value;
    },

    captureRitualFilters(value) {
      this.ritualFilters = value;
    },

    captureConcentrationFilters(value) {
      this.concentrationFilters = value;
    },
    filterSearch(spells, searchText) {
      if (searchText.length > 0) {
        spells = spells.filter(function(spell) {
          let lowerText = searchText.toLowerCase();
          return (
            spell.name
              .toLowerCase()
              .trim()
              .indexOf(lowerText) > -1
          );
        });
      }
      return spells;
    },
    filterSpellBook(spells) {
      if (this.spellBookFilter.length == 0) {
        return spells;
      }

      const filteredSpells = spells.filter(
        c => this.spellBookFilter.indexOf(parseInt(c.id)) !== -1
      );
      return filteredSpells;
    },
    filterClasses(spells) {
      let classFilters = this.classFilters.map(function(value) {
        return value[0].toUpperCase() + value.slice(1);
      });

      if (this.classFilters.length != this.classes.length) {
        spells = spells.filter(function(spell) {
          let x = spell.class.split(",").map(c => {
            return c.trim();
          });
          console.log(x);
          let intersection = x.filter(n => classFilters.includes(n));
          if (intersection.length > 0) {
            return spell;
          }
        });
      }
      return spells;
    },
    filterLevels(spells) {
      let levelFilters = this.levelFilters;
      if (levelFilters.length != this.levels.length) {
        spells = spells.filter(function(spell) {
          let x = spell.level;
          let intersection = levelFilters.includes(parseInt(x));
          if (intersection) {
            return spell;
          }
        });
      }
      return spells;
    },
    filterSources(spells) {
      let sourceFilters = this.sourceFilters;
      if (sourceFilters.length != this.sources.length) {
        spells = spells.filter(function(spell) {
          let source = spell.page.split(" ")[0].toUpperCase();
          console.log(source + ", " + sourceFilters);
          let intersection = sourceFilters.includes(source);
          if (intersection) {
            return spell;
          }
        });
      }
      return spells;
    },
    filterSchools(spells) {
      let schoolFilters = this.schoolFilters;
      if (schoolFilters.length != this.schools.length) {
        spells = spells.filter(function(spell) {
          let x = spell.school;
          let intersection = schoolFilters.includes(x);
          if (intersection) {
            return spell;
          }
        });
      }
      return spells;
    },
    filterConcentration(spells) {
      let concentrationFilters = this.concentrationFilters;
      if (
        concentrationFilters.includes("True") &&
        concentrationFilters.includes("False")
      ) {
        return spells;
      }
      spells = spells.filter(function(spell) {
        let x = spell.conc;
        console.log(x);
        if (concentrationFilters.includes("True") && spell.conc === true) {
          return spell;
        }
        if (
          concentrationFilters.includes("False") &&
          spell.conc === undefined
        ) {
          return spell;
        }
      });
      return spells;
    },
    filterRitual(spells) {
      let ritualFilters = this.ritualFilters;
      if (ritualFilters.includes("True") && ritualFilters.includes("False")) {
        return spells;
      }
      spells = spells.filter(function(spell) {
        let x = spell.ritual;
        console.log(x);
        if (ritualFilters.includes("True") && spell.ritual === true) {
          return spell;
        }
        if (ritualFilters.includes("False") && spell.ritual === undefined) {
          return spell;
        }
      });
      return spells;
    },
    parseSpells(url) {
      fetch(url)
        .then(response => {
          if (!response.ok) {
            throw new Error("HTTP error " + response.status);
          }
          return response.json();
        })
        .then(data => {
          this.spells = data.sort((a, b) => {
            const concatA =
              a.level != "Cantrip" ? `${a.level}${a.name}` : `${0}${a.name}`;
            const concatB =
              b.level != "Cantrip" ? `${b.level}${a.name}` : `${0}${b.name}`;
            return concatA > concatB;
          });
          this.dataLoading = false;
        });
    },
    parseUrl() {
      let uri = window.location.search.substring(1);
      let params = new URLSearchParams(uri);
      let spellsString = params.get("spellbook");
      let spellBookTitle = params.get("spellbookname");
      if (spellsString) {
        this.spellBookFilter = spellsString.split("-").map(c => parseInt(c));
      }
      if (spellBookTitle) {
        this.spellBookTitle = spellBookTitle + ` - ${this.spellBookTitle}`;
        this.navTitle = spellBookTitle;
      }

      console.log(this.spellBookTitle);
    },
    resetInfiniteScroll() {
      this.cardCount = this.pageSize;
      this.updateLastElementYPosition();
    },
    updateLastElementYPosition() {
      this.$nextTick(() => {
        const element = document.querySelector(".infinite-button");
        this.lastElemY = this.getPosition(element).y;
      });
    },
    getPosition(element) {
      let xPosition = 0;
      let yPosition = 0;

      while (element) {
        xPosition +=
          element.offsetLeft - element.scrollLeft + element.clientLeft;
        yPosition += element.offsetTop - element.scrollTop + element.clientTop;
        element = element.offsetParent;
      }

      return { x: xPosition, y: yPosition };
    },
    scroll() {
      const self = this;

      window.addEventListener("resize", this.updateLastElementYPosition());

      window.onscroll = () => {
        const scrollHeight = document.body.scrollHeight;
        const totalHeight = window.scrollY + window.innerHeight;

        // console.log(
        //   `scrollHeight ${scrollHeight}, totalHeight ${totalHeight}, lastelement ${this.lastElemY}`
        // );
        if (totalHeight >= Math.max(scrollHeight, this.lastElemY)) {
          this.updateLastElementYPosition();
          this.$nextTick(() => {
            self.cardCount += this.pageSize; // replace it with your code
          });
        }
      };
    },
    showOutline(e) {
      if (e.keyCode === 9) {
        // the "I am a keyboard user" key
        document.body.classList.add("user-is-tabbing");
        window.removeEventListener("keydown", this.showOutline);
      }
    }
  },
  watch: {
    searchText() {
      this.resetInfiniteScroll();
    },
    classFilters() {
      this.resetInfiniteScroll();
    },
    levelFilters() {
      this.resetInfiniteScroll();
    },
    sourceFilters() {
      this.resetInfiniteScroll();
    },
    schoolFilters() {
      this.resetInfiniteScroll();
    },
    ritualFilters() {
      this.resetInfiniteScroll();
    },
    concentrationFilters() {
      this.resetInfiniteScroll();
    }
  },
  mounted() {
    window.addEventListener("keydown", this.showOutline);
    this.parseUrl();
    const url = "https://spells.red/pastebin";
    // const url = "/spell_data_trimmed.json";

    this.parseSpells(url);
    this.updateLastElementYPosition();
    this.scroll();
  }
};
</script>

<style>
:root {
  --white: #fff;
  --gray: #777;
  --gray-dark: #484848;
  --primary: #bb212b;
  --primary-hover: #e80817;
  --primary-focus: #a51d26;
  --secondary: #795c62;
  --success: #f5be47;
  --success-hover: #fbae06;
  --success-focus: #e39f0c;
  --info: #6e93b6;
  --info-hover: #5594cf;
  --info-focus: #486c8e;
  --warning: #ed7b5f;
  --danger: #127ba3;
  --light: #f8f9fa;
  --dark: #bc524e;
  --breakpoint-xs: 0;
  --breakpoint-sm: 576px;
  --breakpoint-md: 768px;
  --breakpoint-lg: 992px;
  --breakpoint-xl: 1200px;
  --font-family-sans-serif: Asap, Segoe UI, Roboto, Helvetica Neue, Arial,
    sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol;
  --font-family-monospace: SFMono-Regular, Menlo, Monaco, Consolas,
    "Liberation Mono", "Courier New", monospace;
}
html,
body {
  height: 100%;
}
.nav-bg {
  background: var(--white);
}
.btn:hover,
.btn-success:hover,
.btn-danger:hover,
.btn-info:hover,
.btn-dark:hover,
.btn-warning:hover,
.btn-secondary:hover,
.btn-primary:hover {
  transform: scale(1.025);
}
.btn,
.btn-success,
.btn-danger,
.btn-info,
.btn-warning,
.btn-dark,
.btn-secondary,
.btn-primary {
  transition: 0.15s ease-in-out;
}
.btn-sm.close-button {
  border-radius: 0.3rem;
}
input.form-control,
.input-group-prepend .input-group-text {
  border-radius: 0.25rem;
}
#page-content-wrapper .page-item:first-child .page-link {
  border-top-left-radius: 0.4rem;
  border-bottom-left-radius: 0.4rem;
}
#page-content-wrapper .page-item:last-child .page-link {
  border-top-right-radius: 0.4rem;
  border-bottom-right-radius: 0.4rem;
}
.navbar-toggler {
  margin-right: 0px !important;
}
.modal-dialog .modal-header {
  border-bottom: none;
}
.modal-dialog .modal-content {
  border: none;
  border-radius: 0.4rem;
}

#favoritesModal .modal-dialog .modal-content {
  min-height: calc(100vh - 3.5rem);
  height: calc(100vh - 3.5rem);
}
#favoritesModal .modal-body {
  height: calc(100% - 9rem);
}

#app h1,
#app h2,
#app h3,
#app h4,
#app h5,
#app h6,
#app .h1,
#app .h2,
#app .h3,
#app .h4,
#app .h5,
#app .h6 {
  font-weight: 400;
}
body.modal-open {
  padding-right: 0px !important;
  overflow: auto;
}
.nav-bar .modal.fade .modal-dialog {
  -webkit-transition: all 0.125s ease-in-out;
  -moz-transition: all 0.125s ease-in-out;
  transition: all 0.125s ease-in-out;
}

.modal.fade.in .modal-dialog {
  -webkit-transition: all 3s ease;
  -moz-transition: all 3s ease;
  transition: all 3s ease;
}
@media print {
  .print-d-none {
    display: none;
  }
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  margin-top: 60px;
}

a {
  color: #007bff;
}
/* 
button.close {
  background-position: center;
  transition: background 0.8s;
  border-radius: 4rem;
}
button.close:active {
  background-color: #6eb9f7;
  background-size: 90%;
  

  transition: background 0s;
}
button.close:hover {
  background: #47a7f5 radial-gradient(circle, transparent 1%, #47a7f5 1%)
    center/15000%;
} */
body:not(.user-is-tabbing) button:focus,
body:not(.user-is-tabbing) input:focus,
body:not(.user-is-tabbing) select:focus,
body:not(.user-is-tabbing) textarea:focus {
  outline: none;
}

.input-group > .custom-select:not(:last-child),
.input-group > .form-control:not(:last-child) {
  border-radius: 0.5rem !important;
}
#app {
  margin-right: 15px;
  margin-right: 15px;
}

/* SIDEBAR */
html {
  font-size: 1.1rem;
  /* background-image: linear-gradient(-20deg, #ff2846 0%, #6944ff 100%); */
  /* background-image: linear-gradient(-20deg, #ff2846 0%, #6944ff 100%); */
  height: 100%;
}
html > body {
  background: var(--light);
}
#wrapper {
  padding-right: 0;
  -webkit-transition: all 0.5s ease;
  -moz-transition: all 0.5s ease;
  -o-transition: all 0.5s ease;
  transition: all 0.5s ease;
}

#sidebar-wrapper {
  z-index: 1029;
  position: fixed;
  height: calc(100% - 2rem);
  overflow-y: auto;
  -webkit-transition: all 0.5s ease-in-out;
  -moz-transition: all 0.5s ease-in-out;
  -o-transition: all 0.5s ease-in-out;
  transition: all 0.5s ease-in-out;
  margin-top: -2rem;
}

#page-content-wrapper {
  width: 100%;
  position: absolute;
  padding: 0px;
}

#wrapper.toggled #page-content-wrapper {
  position: absolute;
  /* margin-left: -380px; */
}

/* Sidebar Styles */

.sidebar-nav {
  position: absolute;
  top: 0;
  width: 380px;
  margin: 0;
  padding: 0;
  list-style: none;
}

.sidebar-nav > .sidebar-brand {
  height: 65px;
  font-size: 18px;
  line-height: 60px;
}
.custom-checkbox,
.custom-control-label {
  cursor: pointer;
}
.sidebar-nav > .sidebar-brand a {
  color: #999999;
}

.sidebar-nav > .sidebar-brand a:hover {
  color: var(--white);
  background: none;
}

#sidebar-wrapper {
  width: 100vw;
  transform: translateX(100vw);
}
#wrapper.toggled #sidebar-wrapper {
  transform: translateX(0vw);
}

:root {
  --modal-container-footer: 14rem;
  --info-footer: 3em;
  --info-column-gap: 2rem;
}

.modal-xl {
  min-width: 95%;
  /* margin: 5% !important; */
}

h1,
h2,
h3,
h4,
h5,
h6,
.h1,
.h2,
.h3,
.h4,
.h5,
.h6 {
  font-weight: 400;
}
@media (min-width: 768px) {
  #wrapper {
    padding-right: 0;
  }

  #sidebar-wrapper {
    width: 40vw;
    transform: translateX(100vw);
  }
  #wrapper.toggled #sidebar-wrapper {
    transform: translateX(60vw);
  }
  #page-content-wrapper {
    position: relative;
  }
  #wrapper.toggled #page-content-wrapper {
    position: relative;
    margin-left: 0;
  }
}
.infinite-button {
  margin: 0 auto;
}
</style>

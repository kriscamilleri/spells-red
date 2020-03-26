<template>
  <div>
    <b-modal
      title="Create Spellbook"
      id="favoritesModal"
      size="xl"
      method="post"
      ref="favoritesModal"
      class
    >
      <div class="d-flex flex-column body-container">
        <div class="grid-container">
          <div class="grid-top">
            <p class="d-block">
              Select as many spells as you'd like from below, give your Spellbook
              a name, and click Open Spellbook to open a new page with only the
              spells you selected.
            </p>
          </div>
          <div class="grid-left-top border border-primary shadow-sm">
            <div class id="yourSpellbook">
              <div class>
                <div class="added-container">
                  <div class="row">
                    <div class="col-lg-12">
                      <div class="m-3">
                        <h5 class="mb-3">{{ spellBookTitle }}</h5>
                        <!-- 
                      <input
                        class="input mt-2"
                        placeholder="ex. Perrywook's Book"
                        v-model="spellBookName"
                        />-->
                        <label class="sr-only" for="inline-form-input-username">Spellbook Name</label>
                        <b-input-group prepend="Spellbook Name" class="mb-2 mr-sm-2 mb-sm-0">
                          <b-input
                            id="inline-form-input-username"
                            placeholder="Name your spellbook"
                            v-model="spellBookName"
                          ></b-input>
                        </b-input-group>
                        <h5 class="mt-4">Added Spells</h5>
                        <p v-if="addedSpells.length < 1">No spells selected.</p>

                        <span
                          v-for="spell in addedSpells"
                          v-on:click="removeSpell(spell.id)"
                          :key="spell.id"
                          class="btn btn-dark mt-2 mr-2 border-primary border"
                        >
                          <small>{{ spell.level }}</small>
                          {{ spell.name }}
                          <small
                            class="border-primary border-left px-1"
                          >X</small>
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="grid-left-bottom border border-secondary shadow-sm p-3">
            <div class="input-group">
              <div class="input-group-prepend">
                <span class="input-group-text border-bottom-square">Search</span>
              </div>
              <input
                type="text"
                v-model="searchString"
                class="form-control border-bottom-square"
                placeholder="Search for spells"
              />
            </div>
            <div class="search-container-parent border">
              <div class="search-container border">
                <div class="card-sm">
                  <ul class="list-group list-group-flush">
                    <li
                      class="list-group-item"
                      :class="selectedSpell.id === spell.id ? 'selected' : ''"
                      v-for="spell in filteredSpells"
                      v-on:click="selectSpell(spell.id)"
                      :spell="spell"
                      :key="spell.id"
                    >{{ spell.name }}</li>
                    <li class="list-group-item" v-if="filteredSpells.length === 0">No spells found.</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
          <div class="grid-right border border-secondary shadow-sm">
            <div id="infoContainer" class="info-container">
              <div id="infoSubcontainer" class="info-subcontainer align-self-top">
                <span class="info-header">
                  <h3>{{ selectedSpell.name }}</h3>
                  <h5
                    v-if="selectedSpell.level"
                  >Level {{ selectedSpell.level }} - {{ selectedSpell.school }}</h5>
                  <small class="text-primary">{{ selectedSpell.class }}</small>
                </span>
                <!-- <div
                  class="text-justify"
                  :class="this.selectedSpell.desc ? '' : 'invisible'"
                  v-html="extendedDescription"
                ></div>-->
                <span class="extended-description text-justify">
                  <strong v-if="selectedSpell.id" class="d-block pt-2 text-primary">Description</strong>
                  <span v-html="formattedDescription"></span>
                  <strong v-if="selectedSpell.id" class="text-primary">Classes</strong>
                  <p>{{ selectedSpell.class }}</p>
                  <strong v-if="selectedSpell.id" class="text-primary">Components</strong>
                  <p v-html="formattedComponents"></p>
                  <strong v-if="selectedSpell.id" class="text-primary">School</strong>
                  <p>{{ selectedSpell.school }}</p>
                </span>
              </div>
            </div>
            <div
              v-if="paginationEnabled === true"
              class="info-footer border-top p-2 pb-5 text-center align-self-end"
            >
              <span
                class="btn btn-dark float-right"
                :class="nextEnabled ? '' : 'disabled'"
                v-on:click="nextPage()"
              >Next</span>
              <span
                class="btn btn-dark float-left"
                :class="previousEnabled ? '' : 'disabled'"
                v-on:click="previousPage()"
              >Previous</span>
              <span
                :class="selectedSpell.id === undefined ? 'disabled' : ''"
                class="btn btn-primary align-bottom"
                v-on:click="addSpell(selectedSpell)"
              >Add Spell</span>
            </div>
          </div>
        </div>
      </div>
      <template slot="modal-footer">
        <div>
          <b-button
            variant="success"
            size
            class="mx-1"
            :class="addedSpells.length === 0 ? 'disabled' : 'false'"
            :href="generatedLink"
            target="_blank"
          >Open Spellbook</b-button>
          <b-button variant="dark" size class="float-right mx-1" v-on:click="hideModal">Cancel</b-button>
        </div>
      </template>
    </b-modal>
  </div>
</template>

<script>
export default {
  name: "FavoritesModal",
  props: {
    spells: Array
  },
  data() {
    return {
      searchString: "",
      spellBookName: "",
      selectedSpell: {},
      addedSpells: [],
      containerWidth: Number,
      pageCount: 0,
      paginationEnabled: true,
      nextEnabled: true,
      previousEnabled: true
      // generatedLink: ""
    };
  },
  computed: {
    filteredSpells: function() {
      const searchString = this.searchString.toLowerCase();
      let spells = this.spells.filter(
        c => c.name.toLowerCase().indexOf(searchString) != -1
      );
      return spells;
    },
    extendedDescription: function() {
      if (this.selectedSpell) {
        return this.selectedSpell.desc + "<p class='extended-description'></p>";
      }
      return "<p class='extended-description'></p>";
    },
    generatedLink: function() {
      const spellList = this.addedSpells.map(c => c.id);
      const joinedSpells = spellList.join("-");
      const url = `${window.location.protocol}//${window.location.host}?spellbookname=${this.spellBookName}&spellbook=${joinedSpells}`;
      return url;
    },
    spellBookTitle() {
      return this.spellBookName.length > 0
        ? `${this.spellBookName}${
            this.spellBookName[this.spellBookName.length - 1].toLowerCase() !==
            "s"
              ? "'s"
              : "'"
          } Spellbook`
        : "Your Spellbook";
    },
    formattedSubtitle: function() {
      return (
        this.formattedLevel +
        ", " +
        this.formattedRange +
        ",<br> " +
        this.formattedDuration +
        ", " +
        this.formattedcasting +
        "."
      );
    },
    formattedLevel: function() {
      return (
        "<strong class='text-danger'>" + this.selectedSpell.level + "</strong>"
      );
    },
    formattedRange: function() {
      return (
        "<strong class='text-info'>Range&nbsp;</strong>" +
        this.selectedSpell.range.replace("feet", "Feet")
      );
    },
    formattedDetails: function() {
      let description = this.formattedDescription;
      let classes = this.formattedClasses;
      let components = this.formattedComponents;
      return description + classes + components;
    },
    formattedClasses: function() {
      // let classArr = this.spell.class;
      let classArray = this.selectedSpell.class.split(",");

      let classes =
        "<strong>Classes</strong><p>" + classArray.join(", ") + ".</p>";

      return classes;
    },
    formattedDescription: function() {
      if (this.selectedSpell.id) {
        let description = this.convertJsonArrayToHtml(this.selectedSpell.desc);
        if (this.selectedSpell.higher) {
          let higher =
            "<strong>At Higher Levels </strong>" +
            this.convertJsonArrayToHtml(this.selectedSpell.higher);
          description += higher;
        }
        return description;
      }
      return "";
    },
    formattedDuration: function() {
      let result = "<strong class='text-warning'>Duration&nbsp;</strong>";
      if (this.selectedSpell.conc && this.selectedSpell.duration.length > 0) {
        let cleanedDuration = this.selectedSpell.duration;
        cleanedDuration = cleanedDuration.replace("conc, ", "");
        result += cleanedDuration[0].toUpperCase() + cleanedDuration.slice(1);
      } else {
        result += this.selectedSpell.duration;
      }
      return result; // capitalize first letter
    },
    formattedcasting: function() {
      return (
        "<strong class='text-success'>Casting&nbsp;</strong>" +
        this.selectedSpell.casting
      );
    },
    formattedComponents: function() {
      let array = [];
      if (this.selectedSpell.components) {
        if (this.selectedSpell.components.includes("V")) {
          array.push("Verbal");
        }
        if (this.selectedSpell.components.includes("S")) {
          array.push("Somatic");
        }
        if (this.selectedSpell.components.includes("M")) {
          array.push(`Material (${this.selectedSpell.material})`);
        }
        if (array.length > 0) {
          return "<p>" + array.join(", ") + "</p>";
        }
      }
      return "";
    },
    classColor: function() {
      return "muted";
    }
  },
  mounted() {
    // const container = document.getElementsByClassName("info-container")[0];
    // this.containerWidth = container.offsetWidth;
    // window.addEventListener("resize", this.handleResize);

    const self = this;
    let executable;
    window.onresize = function() {
      clearTimeout(executable);
      executable = setTimeout(self.handleResize, 100);
    };
  },
  methods: {
    removeSpell(id) {
      let spells = [];
      for (let i = 0; i < this.addedSpells.length; i++) {
        if (this.addedSpells[i].id !== id) {
          spells.push(this.addedSpells[i]);
        }
      }
      this.addedSpells = spells;
    },
    convertJsonArrayToHtml(jsonArr) {
      let text = this.splitMulti(jsonArr, ["'],['", "'], ['"]);
      text[0] = text[0].replace("['", "");
      text[text.length - 1] = text[text.length - 1].replace(/']/g, "");
      let html = `<p >${text.join("</p><p>")}</p>`;
      return html;
    },
    splitMulti(str, tokens) {
      var tempChar = tokens[0]; // We can use the first token as a temporary join character
      for (var i = 1; i < tokens.length; i++) {
        str = str.split(tokens[i]).join(tempChar);
      }
      str = str.split(tempChar);
      return str;
    },
    hideModal: function() {
      this.$refs["favoritesModal"].hide();
    },
    selectSpell: function(id) {
      this.selectedSpell = this.spells.find(c => c.id === id);
      this.resetPage();
    },
    addSpell: function(spell) {
      //disable button when nothing to add
      if (this.selectedSpell.id === undefined) {
        return;
      }
      const existingSpell = this.addedSpells.find(x => x.id === spell.id);
      if (existingSpell === undefined) {
        this.addedSpells.push(spell);
      }
    },

    previousPage: function() {
      if (!this.previousEnabled) {
        return;
      }
      this.pageCount -= 1;
      const subcontainer = document.getElementById("infoSubcontainer");
      const container = document.getElementById("infoContainer");
      this.containerWidth = container.offsetWidth;
      const translate = this.pageCount * this.containerWidth;
      const translatePadding = this.pageCount > 0 ? 0.25 : 0;
      subcontainer.style.transform = `translateX(calc( -${translate}px - ${translatePadding}rem))`;
      this.togglePaginationButtons();
    },
    nextPage: function() {
      if (!this.nextEnabled) {
        return;
      }
      this.pageCount += 1;
      const subcontainer = document.getElementById("infoSubcontainer");
      const container = document.getElementById("infoContainer");
      this.containerWidth = container.offsetWidth;
      // const translate = this.pageCount * (this.containerWidth + 7.5);
      const translate = this.pageCount * this.containerWidth;
      const translatePadding = this.pageCount > 0 ? 0.25 : 0;
      subcontainer.style.transform = `translateX(calc(-${translate}px - ${translatePadding}rem))`;
      this.togglePaginationButtons();
    },
    togglePaginationButtons: function() {
      //Caution: Hacky/Fragile stuff ahead
      let self = this;
      const executable = function() {
        const infoContainer = document.getElementById("infoContainer");
        const subcontainer = document.getElementById("infoSubcontainer");
        const container = document.getElementById("infoContainer");
        if (container) {
          self.containerWidth = container.offsetWidth;
        }
        let lastChild = subcontainer.lastChild.lastChild;
        lastChild =
          lastChild.innerHTML === "" ? lastChild.previousSibling : lastChild;
        const infoContainerPosRight = self.getElementOffset(infoContainer)
          .right;
        const lastChildPosRight = self.getElementOffset(lastChild).right;
        const scrollDistance = self.pageCount * (self.containerWidth + 15);
        const nextPage = lastChildPosRight > infoContainerPosRight + 17;
        self.nextEnabled = nextPage;
        const previousPage = scrollDistance > 0;
        self.previousEnabled = previousPage;
      };
      this.delayExecution(100, executable);
    },
    resetPage: function() {
      this.paginate();
      this.togglePaginationButtons();
      this.pageCount = 0;
    },
    paginate: function() {
      let self = this;
      //TODO: should turn into its own component, however, for now - a timeout should give vue enough time to add the elements to the DOM and allow for position calculations
      const executable = function() {
        const subcontainer = document.getElementById("infoSubcontainer");
        const container = document.getElementById("infoContainer");
        self.containerWidth = container.offsetWidth;
        let lastChild =
          subcontainer.lastChild.lastChild.innerHTML === ""
            ? lastChild.previousSibling
            : lastChild;

        subcontainer.style.transform = `translateX(0px)`;
        self.pageCount = 0;
        // if (previousPage || nextPage) {
        //   self.paginationEnabled = true;
        // } else {
        //   self.paginationEnabled = false;
        // }
      };
      this.delayExecution(10, executable);
    },
    getElementOffset: function(el) {
      const rect = el.getBoundingClientRect();
      return {
        top: rect.top + window.pageYOffset,
        left: rect.left + window.pageXOffset,
        bottom: rect.top + rect.height + window.pageYOffset,
        right: rect.left + rect.width + window.pageXOffset
      };
    },
    handleResize: function() {
      const subcontainer = document.getElementById("infoSubcontainer");
      const container = document.getElementById("infoContainer");
      this.containerWidth = container ? container.offsetWidth : 0;
      this.resetPage();
      if (subcontainer) {
        subcontainer.style.transform = ``;
      }
    },
    delayExecution(milliseconds, executable) {
      const self = this;
      setTimeout(function() {
        executable(self);
      }, milliseconds);
    }
  }
};
</script>
<style scoped>
@media only screen and (min-width: 600px) {
  .modal-lg .modal-content {
    min-height: 100vh;
    padding: 3rem;
  }

  .modal .grid-container {
    grid-template-columns: 1fr 1fr;
    grid-template-rows: auto 1fr 1fr;
    grid-template-areas:
      "top top"
      "left-top right"
      "left-bottom right";
  }

  .modal .grid-container > div {
    height: 100%;
  }

  .modal .close {
    margin-top: -3rem;
    margin-right: -3rem;
    font-size: 2rem;
  }
}
.grid-top {
  grid-area: top;
}
.grid-left-top {
  grid-area: left-top;
  /* background-color: red; */
}
.grid-left-bottom {
  grid-area: left-bottom;
  /* background-color: green; */
  overflow: auto;
}
.grid-right {
  grid-area: right;
  overflow: auto;
  /* background-color: blue; */
}
.border-bottom-square {
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}
.grid-container {
  display: grid;
  grid-gap: 1rem;
  height: 100%;
  grid-template-columns: 1fr;
  grid-template-rows: auto 20rem 20rem 20rem;
  grid-template-areas:
    "top"
    "left-top"
    "left-bottom"
    "right";
  /* background-color: #fff; */
  color: #444;
  justify-content: space-between;
  align-content: space-between;
}
.grid-container > div {
  /* height: 20rem; */
}
.body-container {
  height: 100%;
  overflow: auto;
}
.max-height {
  /* height: calc(100% - 3rem); */
}
.extended-description:after {
  content: "&nbsp";
  visibility: hidden;
}
.added-container {
  /* max-height: 25rem; */
  overflow-y: scroll;
  overflow-x: hidden;
}
.close-button {
  font-size: 60%;
}
.modal .badge {
  font-size: 130%;
}
/* 
input.form-control {
  padding: 0.75rem 1.25rem; */
/* } */
.selected {
  color: var(--white);
  background-color: var(--primary);
}
.info-container {
  height: calc(100% - var(--info-footer) - 0.6rem);
  padding: 1rem;
  /* max-height: calc(85vh - (var(--info-footer) + var(--modal-container-footer))); */
  -webkit-column-width: 20rem;
  -moz-column-width: 20rem;
  column-width: 20rem;
  break-inside: avoid-page;
  -moz-column-fill: auto;
  column-fill: auto;
  -webkit-column-gap: var(--info-column-gap);
  -moz-column-gap: var(--info-column-gap);
  column-gap: var(--info-column-gap);
  -webkit-column-rule: 1px dotted var(--dark);
  -moz-column-rule: 1px dotted var(--dark);
  column-rule: 1px dotted var(--dark);
  overflow: hidden;
  text-overflow: ellipsis;
}
.info-container .info-header {
  -webkit-column-span: all;
  -moz-column-span: all;
  column-span: all;
}
.info-subcontainer {
  text-overflow: ellipsis;
  /* max-height: calc(85vh - (var(--info-footer) + var(--modal-container-footer))); */
}
.info-container {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
.info-subcontainer::-webkit-scrollbar {
  height: 0;
  display: none;
}
.info-footer {
  height: var(--info-footer);
  width: 100%;
  /* margin-top: -calc(
    80vh - (var(--info-footer) + var(--modal-container-footer))
  ); */
  margin-top: auto;
}
.search-container-parent {
  overflow: auto;
  height: calc(100% - 2.5rem);
}
.search-container {
  -webkit-user-select: none; /* Chrome all / Safari all */
  -moz-user-select: none; /* Firefox all */
  -ms-user-select: none; /* IE 10+ */
  user-select: none;
  /* height: 15rem; */
  overflow: scroll;
}
.search-container li.list-group-item {
  border-radius: 0.25rem;
  cursor: pointer;
}

.search-container li:active {
  background-color: var(--primary-focus);
}

.search-container li:hover {
  background-color: var(--primary-hover);
  color: var(--white);
}
.input-group > .custom-file,
.input-group > .custom-select,
.input-group > .form-control {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}

.modal .input-group .input {
  width: 100%;
  max-width: 100%;
}

.modal .input-group {
  width: 100%;
  max-width: 100%;
}

.max-width-override {
  max-width: 100%;
}

.modal {
  padding-right: 0 !important;
}

.modal-lg .modal-dialog {
  min-width: 100%;
  margin: 0;
}
.modal .custom-select {
  background: none;
}

.modal-body {
  padding: 1rem;
  margin-top: 0.5rem;
}
</style>

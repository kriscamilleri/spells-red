<template>
  <b-modal
    title="Filters & Sorting"
    id="filtersModal"
    size="md"
    method="post"
    ref="filtersModal"
    class
  >
    <b-row>
      <b-col sm="12"><h5 class="text-primary">Sorting</h5></b-col>
    </b-row>
    <b-row>
      <b-col sm="12" class="pb-4">
        <b-row>
          <span class="col-11 align-text-bottom">
            <strong class="filter-header">Option</strong>
            <hr />
          </span>
        </b-row>
        <b-form-group>
          <b-form-radio-group
            v-model="sortFieldSelected"
            class="faux-column row"
            :options="getSortFilterOptions"
          >
          </b-form-radio-group>
        </b-form-group>
      </b-col>
    </b-row>
    <b-row>
      <b-col sm="12"><h5 class="text-primary">Filters</h5></b-col>
    </b-row>
    <b-row class="">
      <div class="scroll-container">
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">Class</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  classAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="classAllSelected"
                  aria-describedby="selectedClassFilters"
                  aria-controls="selectedClassFilters"
                  @change="toggleAllClasses"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedClassFilters"
              class="faux-column row"
              :options="getClassOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">Level</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  levelAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="levelAllSelected"
                  aria-describedby="selectedLevelFilters"
                  aria-controls="selectedLevelFilters"
                  @change="toggleAllLevels"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedLevelFilters"
              class="faux-column row"
              :options="getLevelOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">School</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  schoolAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="schoolAllSelected"
                  aria-describedby="selectedSchoolFilters"
                  aria-controls="selectedSchoolFilters"
                  @change="toggleAllSchools"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedSchoolFilters"
              class="faux-column row"
              :options="getSchoolOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">Source</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  sourceAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="sourceAllSelected"
                  aria-describedby="selectedSourceFilters"
                  aria-controls="selectedSourceFilters"
                  @change="toggleAllSources"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedSourceFilters"
              class="faux-column row"
              :options="getSourceOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">Ritual</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  ritualAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="ritualAllSelected"
                  aria-describedby="selectedRitualFilters"
                  aria-controls="selectedRitualFilters"
                  @change="toggleAllRituals"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedRitualFilters"
              class="faux-column row"
              :options="getRitualOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="12">
          <b-row>
            <span class="col-8 align-text-bottom">
              <strong class="filter-header">Concentration</strong>
              <hr />
            </span>
            <span class="col-4 align-middle pt-2">
              <label
                :class="
                  concentrationAllSelected
                    ? 'btn btn-sm btn-primary'
                    : 'btn btn-sm btn-outline-primary'
                "
              >
                <b-form-checkbox
                  v-model="concentrationAllSelected"
                  aria-describedby="selectedConcentrationFilters"
                  aria-controls="selectedConcentrationFilters"
                  @change="toggleAllConcentrations"
                  class
                  >Select All</b-form-checkbox
                >
              </label>
            </span>
          </b-row>
          <b-form-group>
            <b-form-checkbox-group
              v-model="selectedConcentrationFilters"
              class="faux-column row"
              :options="getConcentrationOptions"
            ></b-form-checkbox-group>
          </b-form-group>
        </b-col>
      </div>
    </b-row>
  </b-modal>
</template>

<script>
export default {
  name: "FiltersModal",
  props: {
    classFilters: Array,
    sourceFilters: Array,
    levelFilters: Array,
    schoolFilters: Array,
    ritualFilters: Array,
    concentrationFilters: Array,
    sortFields: Array,
  },
  data: function () {
    return {
      localClassFilters: this.classFilters,
      selectedClassFilters: this.classFilters,
      localLevelFilters: this.levelFilters,
      selectedLevelFilters: this.levelFilters,
      localSourceFilters: this.sourceFilters,
      selectedSourceFilters: this.sourceFilters,
      localSchoolFilters: this.schoolFilters,
      selectedSchoolFilters: this.schoolFilters,
      localRitualFilters: this.ritualFilters,
      selectedRitualFilters: this.ritualFilters,
      localConcentrationFilters: this.concentrationFilters,
      selectedConcentrationFilters: this.concentrationFilters,
      classAllSelected: true,
      levelAllSelected: true,
      schoolAllSelected: true,
      sourceAllSelected: true,
      concentrationAllSelected: true,
      ritualAllSelected: true,
      selectedSortField: this.sortFields,
      sortFieldSelected: "name",
    };
  },
  computed: {
    getSortFilterOptions() {
      return this.selectedSortField.map((c) => {
        return { text: this.capitalizeFirstLetter(c), value: c };
      });
    },
    getClassOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localClassFilters.length; i++) {
        let row = {
          text:
            this.localClassFilters[i][0].toUpperCase() +
            this.localClassFilters[i].slice(1),
          value: this.localClassFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
    getLevelOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localLevelFilters.length; i++) {
        let row = {
          text: this.localLevelFilters[i],
          value: this.localLevelFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
    getSourceOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localSourceFilters.length; i++) {
        let row = {
          text: this.localSourceFilters[i],
          value: this.localSourceFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
    getSchoolOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localSchoolFilters.length; i++) {
        let row = {
          text: this.localSchoolFilters[i],
          value: this.localSchoolFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
    getRitualOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localRitualFilters.length; i++) {
        let row = {
          text: this.localRitualFilters[i],
          value: this.localRitualFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
    getConcentrationOptions: function () {
      let arr = [];
      for (let i = 0; i < this.localConcentrationFilters.length; i++) {
        let row = {
          text: this.localConcentrationFilters[i],
          value: this.localConcentrationFilters[i],
        };
        arr.push(row);
      }
      return arr;
    },
  },
  methods: {
    capitalizeFirstLetter(string) {
      return string.charAt(0).toUpperCase() + string.slice(1);
    },
    hideModal: function () {
      this.$refs["filtersModal"].hide();
    },
    // toggleSideBar: function () {
    //   this.$emit("sideBarOn", false);
    // },
    toggleAllClasses() {
      this.selectedClassFilters = this.classAllSelected
        ? []
        : this.localClassFilters;
    },
    toggleAllLevels() {
      this.selectedLevelFilters = this.levelAllSelected
        ? []
        : this.localLevelFilters;
    },
    toggleAllSchools() {
      this.selectedSchoolFilters = this.schoolAllSelected
        ? []
        : this.localSchoolFilters;
    },
    toggleAllSources() {
      this.selectedSourceFilters = this.sourceAllSelected
        ? []
        : this.localSourceFilters;
    },
    toggleAllRituals() {
      this.selectedRitualFilters = this.ritualAllSelected
        ? []
        : this.localRitualFilters;
    },
    toggleAllConcentrations() {
      this.selectedConcentrationFilters = this.concentrationAllSelected
        ? []
        : this.localConcentrationFilters;
    },
  },
  watch: {
    selectedClassFilters(newVal) {
      this.classAllSelected =
        this.selectedClassFilters.length == this.localClassFilters.length;

      this.$emit("classFilters", newVal);
    },
    selectedLevelFilters(val) {
      this.levelAllSelected =
        this.selectedLevelFilters.length == this.localLevelFilters.length;

      this.$emit("levelFilters", val);
    },
    selectedSchoolFilters(val) {
      this.schoolAllSelected =
        this.selectedSchoolFilters.length == this.localSchoolFilters.length;

      this.$emit("schoolFilters", val);
    },
    selectedSourceFilters(val) {
      this.sourceAllSelected =
        this.selectedSourceFilters.length == this.localSourceFilters.length;

      this.$emit("sourceFilters", val);
    },
    selectedConcentrationFilters(val) {
      this.concentrationAllSelected =
        this.selectedConcentrationFilters.length ==
        this.localConcentrationFilters.length;

      this.$emit("concentrationFilters", val);
    },
    selectedRitualFilters(val) {
      this.ritualAllSelected =
        this.selectedRitualFilters.length == this.localRitualFilters.length;

      this.$emit("ritualFilters", val);
    },
    sortFieldSelected(val) {
      this.$emit("sortField", val);
    },
  },
};
</script>
<style>
.faux-column div {
  flex: 0 0 50%;
  /* max-width: 50%; */
}
.btn-sm.close-button {
  border-radius: 0.3rem;
}
.filter-header {
  margin-top: 10px;
  margin-bottom: -10px;
  display: block;
}
.faux-column.row {
  margin-right: 0px;
  margin-left: 0px;
}
.faux-column.row .custom-control {
  margin-right: 0px;
}
#closeSidebar {
  /* position: fixed; */
}
</style>
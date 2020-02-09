<template>
  <b-navbar toggleable="md" class="nav-bg shadow-sm fixed-top">
    <b-navbar-brand class="mega-brand text-primary" href="#">
      <img src="/img/icons/android-chrome-48x48.png" width="36" class="nav-brand-icon mr-1" />
      {{navTitle}}
    </b-navbar-brand>
    <b-form-input
      size="md"
      class="search-input mini-search"
      type="text"
      @input="toggleSearch"
      v-model="localSearchText"
      placeholder="Search"
    />
    <b-navbar-toggle target="nav_collapse" class="float-right navbar-toggle"></b-navbar-toggle>
    <b-collapse is-nav id="nav_collapse" class>
      <b-navbar-nav>
        <b-nav-item
          href="https://github.com/kriscamilleri/Curiously-Cheddars-5e-Spells-Search"
          class="nav-button my-2 mx-2 btn-dark float-left shadow-sm"
          target="_blank"
          size="sm"
        >
          Github
        </b-nav-item>
      </b-navbar-nav>
      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">
        <!-- <b-nav-item size="sm" class="my-2 mr-4 nav-button btn-secondary" v-b-modal="'spellModal'">Add Spell...</b-nav-item> -->
        <b-nav-item
          size="sm"
          class="my-2 mx-2 nav-button btn-success shadow-sm"
          v-on:click="togglePrint"
        >Print</b-nav-item>
        <b-nav-item
          size="sm"
          class="my-2 mx-2 nav-button btn-primary shadow-sm"
          v-b-modal="'favoritesModal'"
        >Spellbook</b-nav-item>
        <b-nav-item
          href="#menu-toggle"
          class="my-2 mx-2 nav-button btn-info shadow-sm"
          :class="{ active: sideBarOn}"
          size="sm"
          v-on:click.prevent="toggleSideBar"
          id="menu-toggle"
        >Filter</b-nav-item>
      </b-navbar-nav>
      <div class="input-group">
        <b-form-input
          size="md"
          class="search-input"
          type="text"
          @input="toggleSearch"
          v-model="localSearchText"
          placeholder="Search"
        />
        <a target="_self" href="#" class="navbar-brand mini-brand">CC's Spell Search</a>
      </div>
    </b-collapse>
  </b-navbar>
</template>

<script>
export default {
  name: "SpellNavBar",
  data() {
    return {
      printEnabled: false,
      localSearchText: ""
    };
  },
  props: {
    sideBarOn: Boolean,
    navTitle: String
    // spells: []
  },
  methods: {
    toggleSideBar: function() {
      this.$emit("sideBarOn", !this.sideBarOn);
    },
    toggleSearch: function() {
      this.$emit("searchText", this.localSearchText);
    },
    togglePrint: function() {
      console.log("print toggled");
      this.printEnabled = !this.printEnabled;
      this.$emit("printEnabled", this.printEnabled);
    }
  },
  created() {
    this.sideBarOn = false;
  }
};
</script>

<style>
a[target="_blank"]::after {
  content: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAQElEQVR42qXKwQkAIAxDUUdxtO6/RBQkQZvSi8I/pL4BoGw/XPkh4XigPmsUgh0626AjRsgxHTkUThsG2T/sIlzdTsp52kSS1wAAAABJRU5ErkJggg==);
  margin: 0 3px 0 5px;
  filter: invert();  
}
/*Necessary to left align text when nav-collapse is open*/
@media (max-width: 767px) {
  #nav_collapse a {
    margin-top: 0.5rem;
    margin-left: 0.5rem;
    margin-bottom: 0.5rem;
    float: left;
  }
}
 .external-link{
    vertical-align: initial; 
    border: 1px solid #fff
 }
@media (max-width: 767px) {
  #nav_collapse {
    background: white;
    width: 100%;
    min-width: 100%;
    margin-left: -10px;
    padding: 10px;
    border-radius: 0.25rem;
    box-shadow: 0 0.125rem 0.25rem rgba(72, 72, 72, 0.075) !important;
    border: 1px solid #e4e5e7 !important;
  }
  a.mini-brand {
    display: block;
  }
  a.mega-brand {
    display: none;
  }
  #nav_collapse .input-group > input.form-control.search-input {
    width: 100%;
    display: none;
  }
  input.form-control.mini-search {
    display: block;
    width: max-content;
  }
}

a.mini-brand {
  display: none;
}
input.mini-search {
  display: none;
}
.input-group {
  max-width: 20.8rem;
  margin-right: 0.5rem;
}

.navbar-brand {
  margin-left: 0.5rem;
}

.navbar {
  max-height: 3.75rem;
}

.nav-button {
  border-radius: 0.25rem;
}
 .mega-brand:hover  .nav-brand-icon{
  transform: scale(1.1);
}

.nav-brand-icon {
  transition: 0.15s all;
}
.nav-button a {
  color: #fff !important;
  display: inline-block;
  /* font-weight: 400; */
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  user-select: none;
  border: 1px solid transparent;
  padding: 0.375rem 1rem;
  font-size: 1rem;
  line-height: 1.5;
  border-radius: 0.15rem;
  transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out,
    border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
  font-family: "News Cycle", "Arial Narrow Bold", sans-serif;
  /* font-weight: 700; */
}

li .nav-link {
  width: 100%;
  text-align: left;
}
</style>

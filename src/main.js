import Vue from "vue";
import App from "./App.vue";
// import {BootstrapVue} from "bootstrap-vue";
import Badge from "bootstrap-vue";
import Button from "bootstrap-vue";
import Card from "bootstrap-vue";
import Form from "bootstrap-vue";
import FormCheckbox from "bootstrap-vue";
import Pagination from "bootstrap-vue";
import Modal from "bootstrap-vue";
import Nav from "bootstrap-vue";
import Link from "bootstrap-vue";
import Layout from "bootstrap-vue";
import "bootstrap-vue/dist/bootstrap-vue.min.css";
import "./bootstrap-theme/bootstrap.min.css";

Vue.use(
  Badge,
  Button,
  Card,
  Form,
  FormCheckbox,
  Pagination,
  Nav,
  Link,
  Layout,
  Modal
);

new Vue({
  name: "app",
  components: {},
  el: "#app",
  render: h => h(App)
});

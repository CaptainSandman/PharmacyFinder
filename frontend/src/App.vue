<template>
  <div id="app">
    <b-navbar toggleable="md" type="dark" variant="primary">
      <b-navbar-brand to="/">Josh's PharmacyFinder</b-navbar-brand>

      <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

      <b-collapse is-nav id="nav_collapse">
        <b-navbar-nav>
          <b-nav-item to="about">About</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>

    <router-view/>
  </div>
</template>

<script>
import Globals from './globals'

export default {
  name: 'App',
  data() {
    return {
      alreadyLoadedGmaps: false
    }
  },
  mounted() {
    if (!this.alreadyLoadedGmaps) {
      console.log("PharmacyFinder\nCopyright (C) 2018 Josh Kennedy")

      // Inject the Google Maps API so we don't expose the secret.
      var s = document.createElement('script')

      s.type = 'text/javascript'
      s.src = Globals.getCompleteGoogleAPIUrl()

      document.getElementsByTagName('head')[0].appendChild(s);

      this.alreadyLoadedGmaps = true;
    }
  },
}
</script>

<style>
#app {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>

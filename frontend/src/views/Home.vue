<template>
  <div class="home">
    <b-container>
      <div class="map-responsive">
        <iframe width="600" height="450" frameborder="0" style="border:0" :src="'https://www.google.com/maps/embed/v1/place?key=' + gmapsKey + '&q=Student+Center,+12345+College+Blvd,+Overland+Park,+KS+66210'" ></iframe>
      </div>
    </b-container>
  </div>
</template>

<script>
import Globals from '../globals'

export default {
  name: 'home',
  data() {
    return {
      gmapsKey: Globals.getGoogleKey()
    }
  },
  components: {
  }
}
</script>

<style>
.map-responsive {
  overflow: hidden;
  padding-bottom: 56.25%;
  position: relative;
  height: 0;
}

.map-responsive iframe {
  left: 0;
  top: 0;
  height: 100%;
  width: 100%;
  position: absolute;
}
</style>

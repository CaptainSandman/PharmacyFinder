<template>
  <b-list-group-item :active="active">
    <span @click="onClick">
      {{pharmacy.name}}&nbsp;
      <i v-if="!expanded" class="fas fa-chevron-right"></i>
      <i v-else class="fas fa-chevron-down"></i>
    </span>
    <br/>
    <small>{{pharmacy.city}}, {{pharmacy.state}}</small>
    <div class="details" v-if="expanded">
      <small>
        <br/>
        {{pharmacy.address}}<br/>
        {{pharmacy.city}}, {{pharmacy.state}}, {{pharmacy.zip}}
        <span v-if="pharmacy.distance !== undefined">
          <br/>
          {{pharmacy.distance.toFixed(2)}} miles away.
        </span>
      </small>
    </div>
  </b-list-group-item>
</template>

<script>
export default {
  name: 'PharmacyListItem',
  props: [
    "pharmacy",
    "active"
  ],
  data() {
    return {
      expanded: false,
    }
  },
  methods: {
    onClick() {
      this.expanded = !this.expanded; 
    }
  }
}
</script>

<style>

</style>

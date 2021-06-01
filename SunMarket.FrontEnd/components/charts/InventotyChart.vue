<template>
  <apexchart
    type="area"
    width="100%"
    height="300"
    :options="options"
    :series="series"
  ></apexchart>
</template>

<script>
import apexchart from 'vue-apexcharts'

export default {
  components: {
    apexchart,
  },
  props: {
    snapshotTimeline: {
      type: Object,
      default: () => ({
        timeline: [],
        productInventorySnapshots: [],
      }),
    },
  },
  computed: {
    options() {
      return {
        dataLabels: { enable: false },
        fill: {
          type: 'gradient',
        },
        stroke: {
          curve: 'smooth',
        },
        xaxis: {
          categories: this.snapshotTimeline.timeline,
          type: 'datetime',
          labels: {
            datetimeUTC: false,
          },
        },
        tooltip: {
          x: {
            format: 'dd/MM/yy HH:mm',
          },
        },
      }
    },
    series() {
      return this.snapshotTimeline.productInventorySnapshots.map(
        (snapshot) => ({
          name: snapshot.productId,
          data: snapshot.quantityOnHand,
        })
      )
    },
  },
}
</script>

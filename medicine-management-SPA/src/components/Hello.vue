<template>
  <div class="hello">
	<h1>{{ title }}</h1>
	<h2>Medicine Management</h2>
  </div>
</template>

<script>
export default {
  data () {
	  return {
  	  title: 'ABC Pharmacy'
	  }
  }
}
</script>

<style>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #35495E;
}
</style>
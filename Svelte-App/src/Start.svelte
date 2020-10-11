<script>
  import { onMount } from 'svelte';
  import SectionTitle from './components/Title.svelte';
  import { fade } from 'svelte/transition';
  let title = 'Smartlearning demo';
</script>

<section>
  <SectionTitle {title} />
  <h3 transition:fade>Frontend Svelte, backend dot net azure function/web service for identity server login</h3>

</section>

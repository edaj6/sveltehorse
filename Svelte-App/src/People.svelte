<script>
  import { onMount } from 'svelte';
  import { blur, slide, scale, fade, fly } from "svelte/transition";
  import { quintOut } from "svelte/easing";
  let people = [];

	onMount(async () => {
		const response = await fetch(`https://sveltehorsefunctionapp.azurewebsites.net/api/person`);
		people = await response.json();
  });
</script>

<div class="content-container">
  <div class="content-title-group">
    <h2 class="title">People registered</h2>
    <p>
      Load people from azure function
    </p>
    <br />
    <h2>People</h2>
    <p>
    {#if people}
    <ul>
	    {#each people as { firstName, lastName, birthday }, index}
		  <li in:fly={{x: 200, delay: index * 100}}
      out:fly={{ x: -200 }}>
        {index + 1}: {firstName} {lastName} - {birthday}
		  </li>
	    {/each}
    </ul>
    {:else}
    <p>loading...</p>
    {/if}
    </p>
  </div>
</div>

<script>
  import { onMount } from 'svelte';

  let people;

	onMount(async () => {
		const response = await fetch(`https://sveltehorsefunctionapp.azurewebsites.net/api/person`);
		people = await response.json();
  });
</script>

<div class="content-container">
  <div class="content-title-group not-found">
    <h2 class="title">People registered</h2>
    <p>
      Load people from azure function
    </p>
    <br />
    <h2 class="title">People</h2>
    <p>
    {#if people}
    <ul>
	    {#each people as { firstName, lastName, birthday }, i}
		  <li>
        {i + 1}: {firstName} {lastName} - {birthday}
		  </li>
	    {/each}
    </ul>
    {:else}
    <p>loading...</p>
    {/if}
    </p>
  </div>
</div>

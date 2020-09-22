<script>
  import { onMount } from 'svelte';

  let info;

	onMount(async () => {
		const response = await fetch(`https://azurefunctionapp120200917114555.azurewebsites.net/api/hellojson?name=World`);
		info = await response.json();
  });
</script>

<div class="content-container">
  <div class="content-title-group not-found">
    <h2 class="title">Product Wish List</h2>
    <p>
      Tekst ændret for at teste git push to deploy
    </p>
    <p>
     This project was created to help represent a fundamental app written with
      Svelte.
    </p>
    <br />
    <h2 class="title">Resources</h2>
    <p>
     { info.message }
    </p>
  </div>
</div>

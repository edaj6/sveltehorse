<script>
  import { onMount } from 'svelte';
  import { blur, slide, scale, fade, fly } from 'svelte/transition';
  import SectionTitle from './components/Title.svelte';
  import Hoverable from './components/Hoverable.svelte';

  let people;
  let title = 'Personer';
  let url_default = "https://storagejakob.blob.core.windows.net/blob-container-test/default-horse.jpg";
  onMount(async () => {
    const response = await fetch(
      `https://sveltehorsefunctionapp.azurewebsites.net/api/person`,
    );
    people = await response.json();
  });
</script>

<section>
  <SectionTitle {title} />

  <p>Data fra azure function/cosmos db</p>
  <br />
  {#if people}
    <h2>Personer</h2>
    <ul>
      {#each people as { firstName, lastName, birthday, url }, index}
        <li in:fly={{ x: 200, delay: index * 100 }} out:fly={{ x: -200 }}>
          <Hoverable let:hovering={active}>
            {index + 1}: {firstName} {lastName} - {birthday}
            <div class:active>
              {#if active}
                {#if url}
                  <img transition:blur src={url} alt="Min hest" width="200px" />
                {:else}
                  <img transition:blur src={url_default} alt="Ingen hest" width="150px"  />
                {/if}
              {/if}
            </div>
          </Hoverable>
        </li>
      {/each}
    </ul>
  {:else}
    <h2>Loader personer...</h2>
  {/if}

</section>

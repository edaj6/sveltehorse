<script>
  import { onMount } from 'svelte';
  import { blur, slide, scale, fade, fly } from 'svelte/transition';
  import SectionTitle from './components/Title.svelte';
  import Hoverable from './components/Hoverable.svelte';

  let people;
  let title = 'Personer';

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
      {#each people as { firstName, lastName, birthday, uri }, index}
        <li in:fly={{ x: 200, delay: index * 100 }} out:fly={{ x: -200 }}>
          <Hoverable let:hovering={active}>
            <div class:active>
              {#if active}
                <img href={uri} alt="Min hest {uri}" />
              {:else}{index + 1}: {firstName} {lastName} - {birthday}{/if}
            </div>
          </Hoverable>
        </li>
      {/each}
    </ul>
  {:else}
    <h2>Loader personer...</h2>
  {/if}

</section>

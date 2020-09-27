<script>
  import './styles.scss';
  import { Router, Link, Route } from 'svelte-routing';
  import Start from './Start.svelte';
  import People from './People.svelte';

  import { HeaderBar, NavBar, PageNotFound, Redirect } from './components';

  export let url = '';
</script>

<HeaderBar />
<div class="section columns">
  <Router {url}>
    <NavBar />
    <main class="column">
      <div>
        <Route path="/">
          <Redirect path="/start" />
        </Route>
        <Route path="/start" component={Start} />
        <Route path="/people" component={ People } />
        <Route path="**" component={PageNotFound} />
      </div>
    </main>
  </Router>
</div>

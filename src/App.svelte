<script>
	import FeedbackApp from './components/FeedbackApp.svelte';

	let firstName = 'Basic';
	let lastName = 'features';
	let showText = false;
	$: name = `${firstName} ${lastName}`;

	let users = [
		{
			id: '1',
			name: 'Jeff'
		},
		{
			id: '2',
			name: 'Bob'
		},
		{
			id: '3',
			name: 'Jaqueen'
		}
	];

	const toggle = () => {
		firstName = 'jeff';
		showText = !showText;
		if (users.length != 4)
			users = [...users, {id: '4', name: 'Jamal'}];
	}
</script>

<main>
	<h1>Hello {name}!</h1>
	{#if showText}
		<p>Visit the <a href="https://svelte.dev/tutorial">Svelte tutorial</a> to learn how to build Svelte apps.</p>
	{:else}
		<p>no text</p>
	{/if}

	<button on:click={toggle}>Do something</button>

	{#each users as user (user.id)}
		<h3>{user.id}: {user.name}</h3>
	{/each}
	<FeedbackApp />
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>
<script>
    // import {onMount, onDestroy} from 'svelte';
    import { fade, scale } from 'svelte/transition';
    import {FeedbackStore} from '../store';
    import FeedbackItem from "./FeedbackItem.svelte";

    // let feedback = [];
    // const unsub = FeedbackStore.subscribe((data) => feedback = data);

    // onMount(() => {
    //     console.log('mounted');
    // });

    // onDestroy(() => {
    //     unsub();
    // })
</script>

Feedback list
{#each $FeedbackStore as feed (feed.id)} 
    <div in:scale out:fade="{{ duration: 500 }}">
        <FeedbackItem item={feed} />
    </div>
{/each}
function createInvoice(invoice) {
    var container = getContext().getCollection();
    var containerLink = container.getSelfLink();

    var isAccepted = container.queryDocuments(
        containerLink,
        'SELECT * FROM c where c.customerId = "' + invoice.customerId + '" and c.entityType = "Customer"',
        queryCallback
    );

    if (!isAccepted) throw new Error('The query was not accepted by the server.');

    function queryCallback(err, feed) {
        if (err) throw err;

        if (feed && feed.length) {
            container.createDocument(containerLink, invoice, createCallback)
        }
        else {
            getContext().getResponse().setBody(false);
        }
    }

    function createCallback(err) {
        if (err) throw err;

        getContext().getResponse().setBody(true);
    }
}